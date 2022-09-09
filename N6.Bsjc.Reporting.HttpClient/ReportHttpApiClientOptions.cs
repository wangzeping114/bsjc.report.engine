using N6.Bsjc.Reporting.HttpApi.Client.Contracts;
using N6.Core.Abp.Client.HttpApi;
using System;

namespace N6.Bsjc.Reporting.HttpApi.Client
{
    public class ReportHttpApiClientOptions : BsjcHttpApiClientOptions
    {
        public override Type HttpApiClientContractsModuleType { get; set; } = typeof(ReportServicesHttpApiClientContractsModule);

        public override string RemoteServiceName { get; set; } = "ReportService";

    }
}
