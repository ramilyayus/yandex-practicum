using EventsApp;
using EventsApp.Controllers;
using EventsApp.Infrastructure.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddModel();
builder.Services.AddControllers();
builder.Services.AddRepositories();
builder.Services.AddExceptions();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    builder.Host.UseDefaultServiceProvider(options =>
    {
        options.ValidateScopes = true;
        options.ValidateOnBuild = true;
    });
}

app.UseExceptionHandler();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();