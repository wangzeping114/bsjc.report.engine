using DevExpress.XtraReports.Web.QueryBuilder.Services;
using System;

namespace N6.Bsjc.Reporting.Domain.ExceptionHandlers
{
    public class ApplicationQueryBuilderExceptionHandler: QueryBuilderExceptionHandler
    {
        public override string GetUnknownExceptionMessage(Exception ex)
        {
            return $"{ex.GetType().Name} 内部错误,有关详情请看日志记录";
        }
    }
}
