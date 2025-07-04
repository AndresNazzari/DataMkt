using DataMkt.Infrastructure;
using OfficeOpenXml;

namespace DataMkt.API;

public class Program
{
    public static void Main(string[] args)
    {
        var license = new EPPlusLicense();
        license.SetNonCommercialPersonal("Andres J. Nazzari");
        
        var builder = WebApplication.CreateBuilder(args);

        // Agregar servicios de Infraestructura (DbContext)
        builder.Services.AddInfrastructure(builder.Configuration);

        // Servicios web
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            var xmlPath = Path.Combine(AppContext.BaseDirectory, "DataMkt.API.xml");
            c.IncludeXmlComments(xmlPath);
        });
        
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
    }
}