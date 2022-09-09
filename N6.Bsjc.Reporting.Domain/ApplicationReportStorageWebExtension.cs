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
            // ȷ���Ƿ���Ա������ָ��URL�ı��� .
            // Ӧ����ֻ���ı�����ӷ���**false**���Զ����߼� .
            // �������Ҫ��֤���򷵻�**true**.
            // �������ֻ����Ч��url����(���**IsValidUrl**��������**true**).
            return true;
        }

        public override bool IsValidUrl(string url)
        {
            // ȷ�����ݸ���ǰ����洢��URL�Ƿ���Ч.
            // ʵ�����Լ����߼�����ֹ�����ո�������ض��ַ���url 
            // �������Ҫ��֤���򷵻�**true**.
            return Guid.TryParse(url, out var tryId);
        }

        public override Task<byte[]> GetDataAsync(string url)
        {
            return base.GetDataAsync(url);
        }

        public override byte[] GetData(string url)
        {
            // ʹ��ָ����URL���ش洢�ڱ���洢�����еı���������
            // ���**IsValidUrl**��������**true**������ô˷��� 
            // ������ʹ��**GetData**����������ӿͻ��˷��͵ı������ 
            // ������������ڱ���URL�Ĳ�ѯ�ַ�����
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
            // ���ذ�����������(url)����ʾ���Ƶ��ֵ�. 
            // ���������ʹ�ô˷�����䡰�򿪱����͡����汨���Ի���.
            return base.GetUrls();
        }

        public override async Task SetDataAsync(XtraReport report, string url)
        {
            // ��ָ���ı����浽����ָ�����Ƶı���洢�� 
            // (ֻ�������еı���).
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
            // ��������֤�;���ָ��������(URL) .
            // �˷��������������ؽ������(URL) ,
            // ����ı����������ֻ���±���Ż���ø÷���.
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
