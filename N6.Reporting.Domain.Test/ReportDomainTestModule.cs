using N6.Bsjc.Reporting.Domain;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace N6.Bsjc.Reporting.Domain.Test
{
	[DependsOn(
	   typeof(AbpJsonModule),
	   typeof(ReportingDomainModule)
	)]
	public class ReportDomainTestModule: AbpModule
	{

		public override void ConfigureServices(ServiceConfigurationContext context)
		{

		}
	}
}
