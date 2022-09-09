using System;
using DevExpress.DataAccess.Json;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public class UriDataSourceProvider : IDataSourceProvider
	{
		public ReportDataSourceType ReportDataSourceType => ReportDataSourceType.Uri;
		public object CreateDataSrouce(string name, string dataSrouce, string parameterJson = "")
		{
			var jsonSource = new UriJsonSource()
			{
				Uri = new Uri(dataSrouce)
			};
			jsonSource.BuildParameters(parameterJson);
			var datasource = new JsonDataSource()
			{
				JsonSource = jsonSource
			};
			datasource.Fill();
			datasource.Name = name;
			return datasource;
		}
	}
}
