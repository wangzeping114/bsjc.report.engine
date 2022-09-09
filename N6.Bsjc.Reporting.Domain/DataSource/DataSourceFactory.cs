using System.Collections.Generic;
using System.Linq;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;
using Volo.Abp.DependencyInjection;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public class DataSourceFactory : ITransientDependency, IDataSourceFactory
    {
        private IEnumerable<IDataSourceProvider> _dataSourceProviders;
        public DataSourceFactory(IEnumerable<IDataSourceProvider> dataSourceProviders)
        {
            _dataSourceProviders=dataSourceProviders;
        }
        public IDataSourceProvider GetDataSourceProvider(ReportDataSourceType reportDataSourceType)
        {
            return _dataSourceProviders.FirstOrDefault(x => x.ReportDataSourceType == reportDataSourceType);
        }
    }
}
