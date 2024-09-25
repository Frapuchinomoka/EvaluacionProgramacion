using Microsoft.EntityFrameworkCore;
using Rememba.Services.Users.Data;

var builder = WebApplication.CreateBuilder(args);


//agregamos el arcchivo de contexto creado de la base de datos como un servicio para que pueda ser inyectado en cualquier parte de la aplicaci?n,
//adem?s de configurar la cadena de conexi?n extra?da del archivo appsettings.json
builder.Services.AddDbContext<EjemploDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();