using MagicVilla_API;
using MagicVilla_API.Data;
using MagicVilla_API.Repository;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//se agrego "AddNewtonsoftJson()" para poder usar el Endpoint de PATCH
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar para LA Cadena de conexion
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    
});

//Agregar para usar el Automapper que se instaldo con el NUGET
builder.Services.AddAutoMapper(typeof(MappingConfig));

//se debe agregar la Interface de Villa Repository
builder.Services.AddScoped<IVillaRepository, VillaRepository>();

//se debe agregar la Interface de Numero Villa Repository
builder.Services.AddScoped<INumeroVillaRepository, NumeroVillaRepository>();

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
