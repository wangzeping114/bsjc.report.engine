using System.ComponentModel;

namespace N6.Bsjc.Reporting.Domain.Shared.Emuns
{
	/// <summary>
	/// 值源类型,有关详细. <see cref="https://docs.devexpress.com/XtraReports/9997/detailed-guide-to-devexpress-reporting/use-report-parameters/create-a-report-parameter#value-source"/>
	/// </summary>
	[Description("Uri请求参数类型")]
	public enum ReportValueSourceType
	{
		[Description("静态")]
		Static,

		[Description("动态")]
		Dynamic,

		[Description("区域")]
		Range

	}
}
