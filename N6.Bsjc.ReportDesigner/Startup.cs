using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace N6.Bsjc.ReportDesigner
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ReportDesignerWebModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
