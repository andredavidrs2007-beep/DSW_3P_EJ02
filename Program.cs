using DSW_3P_EJ02.Repositories;
using DSW_3P_EJ02.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped<IPersonajeRepository, PersonajeJsonRepository>();
builder.Services.AddScoped<IPersonajeService, PersonajeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    //Mapear el Swagger
    app.UseSwaggerUI(options =>{
    options.SwaggerEndpoint("/openapi/v1.json", "API de Personajes V1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();