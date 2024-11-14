using CrudAPi.Data;
using CrudAPi.Services.Cliente;
using Microsoft.EntityFrameworkCore;
using CrudAPi.Services.Cliente;

var builder = WebApplication.CreateBuilder(args);

// Registrar o serviço com a interface
builder.Services.AddScoped<IClienteInterface, ClienteService>();

var MyallowSpecific = "MyallowSpecific";


// Adicionar o serviço CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyallowSpecific", policy =>
        policy.WithOrigins("http://localhost:5173") // URL do frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()); // Permitir credenciais (caso seja necessário)
});




// Add services to the container.
builder.Services.AddControllers();

// Configuração Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do DbContext para PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usar a política CORS
app.UseCors("MyallowSpecific");

app.UseAuthorization();

app.MapControllers();

app.Run();
