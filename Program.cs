using Microsoft.OpenApi.Models;
using PrtgAPI;
using PrtgProxyApi.Domain;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.PrtgAPISatrack.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddHttpClient();  // HttpClient general
builder.Services.AddMemoryCache();

// Configurar PrtgSettings (asegúrate de que la sección en appsettings.json es correcta)
// Primero: leer la configuración
var prtgSettings = builder.Configuration.GetSection("PrtgApi").Get<PrtgProxyApi.PrtgAPISatrack.Settings.PrtgSettings>();

builder.Services.AddSingleton<PrtgClient>(provider =>
{
    return new PrtgClient(
        prtgSettings.Server,
        prtgSettings.Username,
        prtgSettings.Password,
        AuthMode.Password,
        prtgSettings.IgnoreSSL
    );
});


// Registrar Services correctamente
builder.Services.AddScoped<ISensorsServiceDomain, SensorsServiceDomain>();
builder.Services.AddScoped<IDeviceServiceDomain, DeviceServiceDomain>();

builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

// Configurar Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PRTG API", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBackstage",
        policy => policy.WithOrigins("http://localhost:3000") // URL de Backstage
                        .AllowAnyMethod()
                        .AllowAnyHeader());
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

app.UseAuthorization();
app.UseCors("AllowBackstage");

app.MapControllers();

app.Run();
