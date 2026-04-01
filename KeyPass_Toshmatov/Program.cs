using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Инструментарий"
    });

    //string PathFile = Path.Combine(AppContext.BaseDirectory, "KeePass.xml");
    //options.IncludeXmlComments(PathFile);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Инструментарий");
});

app.UseRouting();
app.MapControllers();

app.Run();