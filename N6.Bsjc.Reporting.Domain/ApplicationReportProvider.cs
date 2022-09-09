using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;
using N6.Bsjc.Reporting.Domain.DataSource;
using N6.Bsjc.Reporting.HttpApi.Client.Contracts.ServiceProxies;
using System;
using System.Threading.Tasks;
using Volo.Abp.Threading;

namespace N6.Bsjc.Reporting.Domain
{
    public class ApplicationReportProvider :BaseApplicationReportProvider, IReportProvider
    {
 
        public ApplicationReportProvider(
            IReportServiceProxy reportServiceProxy,
            IDataSourceFactory dataSourceFactory):base(reportServiceProxy,dataSourceFactory)
        {
 
        }
        public XtraReport GetReport(string id, ReportProviderContext context)
        {
            if (!Guid.TryParse(id, out var verifyId))
            {
                throw new ArgumentNullException($"{nameof(id)} is not guid type!");
            }
            return AsyncHelper.RunSync(() => GetReportAsync(id));
    
        }
    }

    public class ApplicationReportProviderAsync : BaseApplicationReportProvider, IReportProviderAsync
    {
        public ApplicationReportProviderAsync(
            IReportServiceProxy reportServiceProxy,
            IDataSourceFactory dataSourceFactory):base(reportServiceProxy, dataSourceFactory)
        {

        }
        public async Task<XtraReport> GetReportAsync(string id, ReportProviderContext context)
        {
            if (!Guid.TryParse(id, out var verifyId))
            {
                throw new ArgumentNullException($"{nameof(id)} is not guid type!");
            }
            return await GetReportAsync(id);
        }
    }
}
