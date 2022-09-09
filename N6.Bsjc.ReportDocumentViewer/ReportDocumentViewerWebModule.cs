using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.XtraReports.Web.WebDocumentViewer;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N6.Bsjc.Reporting.Domain;
using N6.Bsjc.Reporting.HttpApi.Client;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace N6.Bsjc.ReportDocumentViewer
{
	[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
	typeof(ReportingClientAppModule),
    typeof(ReportingDomainModule)
    )]
    public class ReportDocumentViewerWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // remark: DevExpress of fixed injection mode, do not adjust sequence
            context.Services.AddDevExpressControls();
            //context.Services.AddScoped<ReportStorageWebExtension, ApplicationReportStorageWebExtension>();
            context.Services.AddTransient<IWebDocumentViewerReportResolver, ApplicationWebDocumentViewerReportResolver>();
            context.Services.AddControllersWithViews();
            context.Services.ConfigureReportingServices(configurator =>
            {
                configurator.ConfigureReportDesigner(designerConfigurator =>
                {
                    designerConfigurator.RegisterDataSourceWizardConfigFileConnectionStringsProvider();
                    designerConfigurator.RegisterObjectDataSourceWizardTypeProvider<ApplicationObjectDataSourceWizardTypeProvider>();
                    designerConfigurator.RegisterObjectDataSourceConstructorFilterService<ApplicationObjectDataSourceConstructorFilterService>();
                });
                configurator.ConfigureWebDocumentViewer(viewerConfigurator =>
                {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                });
            });
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();
            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
        
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
			app.UseCustomerExceptionHandler();

			app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

	public static class CustomerExceptionExtesion
	{
		public static void UseCustomerExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(c => c.Run(async context =>
			{
				var exception = context.Features
					.Get<IExceptionHandlerPathFeature>()
					.Error;

				var response = new { error = exception.Message };
				await context.Response.WriteAsJsonAsync(response);
			}));
		}
	}
}
