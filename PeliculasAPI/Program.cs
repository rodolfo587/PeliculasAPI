using Microsoft.EntityFrameworkCore;
using PeliculasAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi(); Se elimina para agregar Swagger (primero instalar desde admon nuget packages)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");
});

builder.Services.AddOutputCache(opciones => 
opciones.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60));

//*****PERMITIR ALGUNOS ORIGENES EN CORS**********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************
var origenesPermitidos = builder.Configuration.GetValue<string>("origenesPermitidos")!.Split(',');

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos).AllowAnyMethod().AllowAnyHeader();
    });
});

//************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************

//*****PERMITIR TODOS LOS ORIGENES EN CORS********************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************
//builder.Services.AddCors(opciones =>
//{
//    opciones.AddDefaultPolicy(opcionesCORS =>
//    {
//        opcionesCORS.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//    });
//});
//************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************************

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi(); despues de agregar las dos lineas de mas arriba para agregar swagger, poner esto:
    app.UseSwagger();
    app.UseSwaggerUI();
    //Luego ir a launchSettings.json y cambiar:
    //"launchUrl": "weatherforecast",
    //por
    //"launchUrl": "swagger ",
}

app.UseHttpsRedirection();

app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();
