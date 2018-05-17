using System.Web.Http;
using WebActivatorEx;
using Empresa.Compras.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Empresa.Compras.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    { 
                        c.SingleApiVersion("v1", "Empresa.Compras.Api");
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
