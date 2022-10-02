using EngineFramework.Microsoft.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseLamarAndPluginEngine((context, services) =>
{
    // Add services to the container.

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
});

builder.Configuration.AddAllJsonFiles();

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


// Integration testing: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0
public partial class Program { }