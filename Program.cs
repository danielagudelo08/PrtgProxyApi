using Microsoft.OpenApi.Models;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Middlewares;
using PrtgProxyApi.Services;
using PrtgProxyApi.Settings;

var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddHttpClient();  // HttpClient general
builder.Services.AddMemoryCache();

// Configurar PrtgSettings (asegúrate de que la sección en appsettings.json es correcta)
builder.Services.Configure<PrtgSettings>(builder.Configuration.GetSection("PrtgApi"));

// Registrar Services correctamente
builder.Services.AddScoped<IDevicesService, DevicesService>();
builder.Services.AddScoped<ISensorsService, SensorsService>();
builder.Services.AddScoped<IGroupsService, GroupsService>();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PRTG API", Version = "v1" });
});

var app = builder.Build();

// Configura la tubería de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Mejor para desarrollo
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PRTG API v1");
    });
}
else
{
    app.UseExceptionHandler("/error");  // Manejador de errores en producción
}

// Middleware global de excepciones (siempre debe estar habilitado)
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
