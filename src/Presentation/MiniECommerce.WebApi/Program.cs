using MiniECommerce.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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


app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
