using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public interface IDataSourceFactory
    {
        IDataSourceProvider GetDataSourceProvider(ReportDataSourceType reportDataSourceType);
    }
}
