
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using DevExpress.DataAccess.Web;

namespace N6.Bsjc.Reporting.Domain
{
    public class ApplicationObjectDataSourceConstructorFilterService : IObjectDataSourceConstructorFilterService
    {
        public IEnumerable<ConstructorInfo> Filter(Type dataSourceType, IEnumerable<ConstructorInfo> constructors)
        {
            if (dataSourceType == typeof(ReportingControlModel))
                return constructors;
            else
                return constructors.Where(x => x.GetParameters().Length > 0);
        }
    }
}
