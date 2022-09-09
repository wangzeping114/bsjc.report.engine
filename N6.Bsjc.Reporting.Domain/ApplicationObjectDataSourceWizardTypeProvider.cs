using System;
using System.Collections.Generic;

using DevExpress.DataAccess.Web;

namespace N6.Bsjc.Reporting.Domain
{
    public class ApplicationObjectDataSourceWizardTypeProvider : IObjectDataSourceWizardTypeProvider
    {
        public IEnumerable<Type> GetAvailableTypes(string context)
        {
            return new[] { typeof(ReportingControlModel) };
        }
    }
}
