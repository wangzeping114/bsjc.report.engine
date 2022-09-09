using System;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public interface IDataSourceProvider
	{
        ReportDataSourceType ReportDataSourceType { get; }


        object CreateDataSrouce(string name,string dataSrouce, string parameterJson="");
    }
}
