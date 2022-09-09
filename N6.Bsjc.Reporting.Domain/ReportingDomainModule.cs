using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.XtraReports.Web.QueryBuilder.Services;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.Extensions.DependencyInjection;
using N6.Bsjc.Reporting.Domain.DataSource;
using N6.Bsjc.Reporting.Domain.ExceptionHandlers;
using Volo.Abp.Modularity;

namespace N6.Bsjc.Reporting.Domain
{
    public class ReportingDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
			var services = context.Services;
			Assembly.GetExecutingAssembly()
				.GetTypesAssignableFrom<IDataSourceProvider>()
				.ForEach(t =>
				{
					services.AddTransient(typeof(IDataSourceProvider), t);
				});

			services.AddScoped<IWebDocumentViewerExceptionHandler, ApplicationWebDocumentViewerExceptionHandler>();
			services.AddScoped<IReportDesignerExceptionHandler, ApplicationReportDesignerExceptionHandler>();
			services.AddScoped<IQueryBuilderExceptionHandler, ApplicationQueryBuilderExceptionHandler>();
        }
    }

	public static class AssemlbyExtesion
	{
		public static List<Type> GetTypesAssignableFrom<T>(this Assembly assembly)
		{
			return assembly.GetTypesAssignableFrom(typeof(T));
		}
		public static List<Type> GetTypesAssignableFrom(this Assembly assembly, Type compareType)
		{
			var ret = new List<Type>();
			foreach (var type in assembly.DefinedTypes)
			{
				if (compareType.IsAssignableFrom(type) && compareType != type)
				{
					ret.Add(type);
				}
			}
			return ret;
		}
	}
}
