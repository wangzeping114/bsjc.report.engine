using DevExpress.XtraReports.Web.ReportDesigner.Services;
using System;
using System.IO;

namespace N6.Bsjc.Reporting.Domain.ExceptionHandlers
{
    public class ApplicationReportDesignerExceptionHandler : ReportDesignerExceptionHandler
    {
        public override string GetExceptionMessage(Exception ex)
        {
            if (ex is FileNotFoundException)
            {

                return "文件未找到";
            }
            return base.GetExceptionMessage(ex);
        }
        public override string GetUnknownExceptionMessage(Exception ex)
        {
            return $"{ex.GetType().Name} 内部错误.有关详情请看日志";
        }
    }
}
