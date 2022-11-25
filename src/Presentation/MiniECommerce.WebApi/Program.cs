using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiniECommerce.Application;
using MiniECommerce.Application.Validations.FluentValidation.Validators;
using MiniECommerce.Infrastructure;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Infrastructure.Services.Storage.Azure;
using MiniECommerce.Infrastructure.Services.Storage.Local;
using MiniECommerce.Persistence;
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

//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddStorage<LocalStorage>();

// CORS Policy'lerin belirlenmesi;
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    corsPolicyBuilder
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();
app.UseHttpsRedirection();

// Authorize attribute'ü ile işaretlediğimiz controller veya method'ların authentication işlemlerini aktifleştirmek için; 
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
