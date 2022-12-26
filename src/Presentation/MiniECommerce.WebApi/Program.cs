using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Application;
using MiniECommerce.Application.Validations.FluentValidation.Validators;
using MiniECommerce.Infrastructure;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Infrastructure.Services.Storage.Azure;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using MiniECommerce.Persistence;
using MiniECommerce.SignalR;
using MiniECommerce.WebApi.Configurations.Serilog.ColumnWriters;
using MiniECommerce.WebApi.Extensions;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// API'ımıza oluşturduğumuz Filter'ı ekliyor ve Default olan ModelStateInvalidFilter'ı eziyoruz.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

// Fluent Validation'ı ekliyoruz;
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSql kullanımı için yazmış olduğumuz extension method;
builder.Services.ConfigureNpgSql(builder.Configuration);

// Service'lerin kullanımı için yazmış olduğumuz extension method'lar;
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddStorage<LocalStorage>();

// CORS Policy'lerin belirlenmesi;
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    corsPolicyBuilder
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials() // SignalR ile client tarafından connection isteklerine izin vermek için ekliyoruz.
));

// Request'lerle gelen token'ın (JWT) doğrulanması için gerekli konfigürasyonlar
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            // Token'ımız doğrulanırken hangi kriterlerin baz alınacağını belirliyoruz.
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            // Token'ımız doğrulanırken yukarıdaki kriterlere karşılık gelecek değerleri belirliyoruz.
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow,

            NameClaimType = ClaimTypes.Email
        };
    });


// Serilog konfigürasyonlarını belirleyip log işlemleri için Host'a Serilog'u kullanacağımızı bildiriyoruz. 
Logger logger = new LoggerConfiguration()
    //.WriteTo.Console()
    //.WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("Npgsql"), "logs", needAutoCreateTable: true,
    columnOptions: new Dictionary<string, ColumnWriterBase>
    {
        { "message", new RenderedMessageColumnWriter() },
        { "message_template", new MessageTemplateColumnWriter() },
        { "level", new LevelColumnWriter() },
        { "timestamp", new TimestampColumnWriter() },
        { "exception", new ExceptionColumnWriter() },
        { "log_event", new LogEventSerializedColumnWriter() },
        { "user_email", new UserEmailColumnWriter() }
    })
    //.WriteTo.Seq(serverUrl: builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext() //LogContext'e pushladığımız property'lerin okunmasını ve database'deki karşılığı olan kolon'a yazılmasını sağlıyoruz.
    .MinimumLevel.Error()
    .CreateLogger();

builder.Host.UseSerilog(logger);


// Built-in gelen http logging mekanızmasının konfigürasyonunu yapıyoruz.
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//Global exception handler için yazmış olduğumuz extension method'ı çağırıyoruz.
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

// Kendinden sonraki middleware'lerde ele alınan request'lerde herhangi bir hata olursa Serilog ile loglanmasını sağlıyoruz.
app.UseSerilogRequestLogging();

// Built-in gelen http logging mekanızmasının kullanımını aktifleştiriyoruz.
app.UseHttpLogging();

app.UseCors();
app.UseHttpsRedirection();

// Authorize attribute'ü ile işaretlediğimiz controller veya method'ların authentication işlemlerini aktifleştirmek için; 
app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    string userEmail = context.User?.Identity?.IsAuthenticated == true ? context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value : null;
    if (userEmail != null)
        LogContext.PushProperty("user_email", userEmail);

    await next();
});

app.MapControllers();

// Tüm hub'larımızın endpoint'lerinin map'lendiği extension method
app.MapHubs();

app.Run();
