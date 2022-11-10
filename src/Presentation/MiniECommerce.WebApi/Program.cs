using FluentValidation;
using FluentValidation.AspNetCore;
using MiniECommerce.Application.Validations.FluentValidation.Validators;
using MiniECommerce.Infrastructure.Filters;
using MiniECommerce.Persistence;

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

// Repository kullanımı için yazmış olduğumuz extension method;
builder.Services.ConfigureRepositories();

// CORS Policy'lerin belirlenmesi;
builder.Services.AddCors(corsOptions => corsOptions.AddDefaultPolicy(corsPolicyBuilder =>
    corsPolicyBuilder
    .WithOrigins("http://localhost:4200", "https://localhost:4200")
    .AllowAnyHeader()
    .AllowAnyMethod()
));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
