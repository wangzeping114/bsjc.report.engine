using DevExpress.XtraReports.Web.WebDocumentViewer;
using System;
using System.IO;

namespace N6.Bsjc.Reporting.Domain.ExceptionHandlers
{
    public class ApplicationWebDocumentViewerExceptionHandler : WebDocumentViewerExceptionHandler
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
            return $"{ex.GetType().Name} 发生错误,有关详细信息请参阅日志文件!";
        }
    }
}
