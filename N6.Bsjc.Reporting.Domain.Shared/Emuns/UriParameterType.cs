using System.ComponentModel;

namespace N6.Bsjc.Reporting.Domain.Shared.Emuns
{
	/// <summary>
	/// <see href="https://docs.devexpress.com/XtraReports/400380/detailed-guide-to-devexpress-reporting/bind-reports-to-data/json-data/bind-a-report-to-json-data-runtime-sample#use-data-source-parameters">使用数据源参数</see>
	/// </summary>
	[Description("Uri请求参数类型")]
	public enum UriParameterType
	{
		[Description("URI:查询参数")]
		Query = 0,

		[Description("URI:路径参数")]
		Path = 1,

		[Description("URI:标头参数")]
		Header = 2,
	}
}
