using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace N6.Bsjc.ReportDocumentViewer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<ReportDocumentViewerWebModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
