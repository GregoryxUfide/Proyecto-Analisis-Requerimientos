using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region BD
builder.Services.AddDbContext<GranHotelDesamparadosContext>(optionsAction =>
                    optionsAction
                    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))

    );

#endregion

#region DI

builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IProductoDAL, ProductoDAL>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IUbicacionProductoDAL, UbicacionProductoDAL>();
builder.Services.AddScoped<IUbicacionProductoService, UbicacionProductoService>();



#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
