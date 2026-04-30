using Microsoft.EntityFrameworkCore;
using SimplifiedStock.API.Middleware;
using SimplifiedStock.Infrastructure.Contexts;
using SimplifiedStock.Infrastructure.Extensions;
using SimplifiedStock.Services.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//Register service layer dependencies
builder.Services.AddServiceLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

//Global exception middleware
app.UseGlobalExceptionHandlingMiddleware();

//Automigrate database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<StockDatabaseContext>();
    db.Database.Migrate();
}

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
