using CrudAPi.Data;
using CrudAPi.Services.Cliente;
using Microsoft.EntityFrameworkCore;
using CrudAPi.Services.Cliente;

var builder = WebApplication.CreateBuilder(args);

// Registrar o serviço com a interface
builder.Services.AddScoped<IClienteInterface, ClienteService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
