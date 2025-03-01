using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using WEBAPPP.Data;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5002");  //forcé lapp a tourné au port 5001 toujours 

//  Ajout des services avant builder.Build()
builder.Services.AddControllers();// tu dit api que tu travail avec des controlleur 
builder.Services.AddHttpClient();  //comunication entre API via HTTPclient
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); //connection a la DB
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")  // Port où tourne React
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()); // Pour SignalR
});
var app = builder.Build(); //  ICI on fige le service !

// Middleware après builder.Build()
if (app.Environment.IsDevelopment()) //le mode de production mazal
{
    app.UseSwagger();
    app.UseSwaggerUI(); //voir si les requte marche 
}

// app.UseHttpsRedirection(); // (Optionnel,  dartha pour  éviter le warning)

// Middleware pour gérer les requêtes
app.UseRouting();
app.UseCors("AllowReactApp"); //activation de CORS 
app.UseAuthorization();
app.MapControllers(); // Permet à tes contrôleurs d'être accessibles via l'API
app.MapGet("/", () => "Hello, ASP.NET API IA Core! Répond parfaitement!"); 

app.Run();
