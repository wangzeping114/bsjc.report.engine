using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.Extensions;
using N6.Bsjc.Reporting.Domain.Events;
using N6.Bsjc.Reporting.HttpApi.Client.Contracts.ServiceProxies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Threading;

namespace N6.Bsjc.Reporting.Domain
{
    public class ApplicationReportStorageWebExtension: ReportStorageWebExtension
    {
        private readonly IReportServiceProxy _reportServiceProxy;
        private readonly IDistributedEventBus _distributedEventBus;
        public ApplicationReportStorageWebExtension(IReportServiceProxy reportServiceProxy,
            IDistributedEventBus distributedEventBus)
        {
            _reportServiceProxy = reportServiceProxy;
            _distributedEventBus = distributedEventBus;
        }
        public override bool CanSetData(string url)
        {
            // 确定是否可以保存具有指定URL的报表 .
            // 应该是只读的报表添加返回**false**的自定义逻辑 .
            // 如果不需要验证，则返回**true**.
            // 这个方法只对有效的url调用(如果**IsValidUrl**方法返回**true**).
            return true;
        }

        public override bool IsValidUrl(string url)
        {
            // 确定传递给当前报表存储的URL是否有效.
            // 实现您自己的逻辑来禁止包含空格或其他特定字符的url 
            // 如果不需要验证，则返回**true**.
            return Guid.TryParse(url, out var tryId);
        }

        public override Task<byte[]> GetDataAsync(string url)
        {
            return base.GetDataAsync(url);
        }

        public override byte[] GetData(string url)
        {
            // 使用指定的URL返回存储在报表存储介质中的报表布局数据
            // 如果**IsValidUrl**方法返回**true**，则调用此方法 
            // 您可以使用**GetData**方法来处理从客户端发送的报告参数 
            // 如果参数包含在报表URL的查询字符串中
            var reportData = AsyncHelper.RunSync(() => _reportServiceProxy.GetReportAsync(Guid.Parse(url)));
            if(reportData != null) 
            {
                return reportData.ReportLayout;
            }
            throw new DevExpress.XtraReports.Web.ClientControls.FaultException($"Could not find report{url}");
        }

        public override Task<Dictionary<string, string>> GetUrlsAsync()
        {
            return base.GetUrlsAsync();
        }

        public override Dictionary<string, string> GetUrls()
        {
            // 返回包含报表名称(url)和显示名称的字典. 
            // 报表设计器使用此方法填充“打开报表”和“保存报表”对话框.
            return base.GetUrls();
        }

        public override async Task SetDataAsync(XtraReport report, string url)
        {
            // 将指定的报表保存到具有指定名称的报表存储中 
            // (只保存现有的报告).
            await _distributedEventBus.PublishAsync(new UpdateReportEvent
            {
                ReportId = Guid.Parse(url),
                ReportLayout = ReportToByteArray(report),
                ReportName = report.Name
            });
        }

        public override void SetData(XtraReport report, string url)
        {
       
       
        }

        public override async Task<string> SetNewDataAsync(XtraReport report, string defaultUrl)
        {
            // 允许您验证和纠正指定的名称(URL) .
            // 此方法还允许您返回结果名称(URL) ,
            // 把你的报告存起来，只有新报表才会调用该方法.
            var createReport = new CreateReportEvent
            {
                ReportLayout = ReportToByteArray(report),
                ReportName = report.Name
            };
            await _distributedEventBus.PublishAsync(createReport);
            return createReport.ReportId.ToString();
        }

        public override string SetNewData(XtraReport report, string defaultUrl)
        {
            return base.SetNewData(report, defaultUrl);
        }

        static byte[] ReportToByteArray(XtraReport report)
        {
            using (var memoryStream = new MemoryStream())
            {
                report.SaveLayoutToXml(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
