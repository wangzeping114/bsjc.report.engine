using System;
using System.Threading.Tasks;
using N6.Bsjc.Reporting.Domain.Shared;
using N6.Core.Abp.Client.Contracts;
using Refit;

namespace N6.Bsjc.Reporting.HttpApi.Client.Contracts.ServiceProxies
{
	public interface IReportServiceProxy: IRefitServiceProxy
    {
        [Get("/api/report-management/getById/{id}")]
        Task<ReportDefineModel> GetReportAsync(Guid id);
     }
}
