using Microsoft.OpenApi.Models;
using DocumentTemplatesWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
builder.Configuration.GetSection("Settings").Bind(settings);
builder.Services.AddSingleton(settings);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo {
    Version = "v1",
    Title = "Documets generator API",
    Description = "API for generating documents from a template"
}));

builder.Services.AddTransient<MSWordService>();
builder.Services.AddSingleton<MongoLoggerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.MapWhen(url => url.Request.Path.Value != null, 
app => {
    app.UseRouting();
    app.UseEndpoints(endpoint => endpoint.MapControllers());
});

app.Run();
