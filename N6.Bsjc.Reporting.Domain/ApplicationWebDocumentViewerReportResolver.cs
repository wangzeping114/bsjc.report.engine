using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using N6.Bsjc.Reporting.Domain.DataSource;
using N6.Bsjc.Reporting.HttpApi.Client.Contracts.ServiceProxies;
using Volo.Abp.Threading;

namespace N6.Bsjc.Reporting.Domain
{
    public class ApplicationWebDocumentViewerReportResolver : BaseApplicationReportProvider, IWebDocumentViewerReportResolver
    {

        public ApplicationWebDocumentViewerReportResolver(
            IReportServiceProxy reportServiceProxy,
            IDataSourceFactory dataSourceFactory):base(reportServiceProxy, dataSourceFactory)
        {
            
        }

        public XtraReport Resolve(string id)
        {
            return AsyncHelper.RunSync(()=>GetReportAsync(id));
        }
    }
}
