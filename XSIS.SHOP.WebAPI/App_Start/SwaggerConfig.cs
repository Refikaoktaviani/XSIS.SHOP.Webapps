using System.Web.Http;
using WebActivatorEx;
using XSIS.SHOP.WebAPI;
using Swashbuckle.Application;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace XSIS.SHOP.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "XSIS.SHOP.WebAPI");

                        //yang ditambahain di swagger config
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
