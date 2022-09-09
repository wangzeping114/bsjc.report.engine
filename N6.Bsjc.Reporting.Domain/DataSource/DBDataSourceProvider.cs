using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public class DBDataSourceProvider : IDataSourceProvider
	{
		public ReportDataSourceType ReportDataSourceType => ReportDataSourceType.DB;

		// TODO
		public object CreateDataSrouce(string name, string dataSrouce, string parameterJson = "")
		{
			throw new System.NotImplementedException();
		}
	}
}
