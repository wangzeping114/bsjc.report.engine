using System;
using DevExpress.DataAccess.Json;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;
using N6.Bsjc.Reporting.Domain.Shared.Extends;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public class JsonDataSourceProvider : IDataSourceProvider
    {
        public ReportDataSourceType ReportDataSourceType => ReportDataSourceType.Json;

		public object CreateDataSrouce(string name, string dataSrouce,
			string parameterJson = "")
		{
			var jsonDataSource = new JsonDataSource();
			jsonDataSource.Name = name;
			jsonDataSource.JsonSource = CreateJsonSource(dataSrouce, parameterJson);
			jsonDataSource.Fill();
			return jsonDataSource;
		}

		private static JsonSourceBase CreateJsonSource(string dataSrouce,string parameterJson)
		{
			if (dataSrouce.IsJson())
			{
				return new CustomJsonSource(dataSrouce);
			}
			var fileUri = new Uri(dataSrouce, UriKind.RelativeOrAbsolute);
			var uriJsonSource = new UriJsonSource(fileUri);
			uriJsonSource.BuildParameters(parameterJson);
			return uriJsonSource;
		}
    }
}
