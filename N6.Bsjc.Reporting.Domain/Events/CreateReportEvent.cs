using System;
using Volo.Abp.EventBus;

namespace N6.Bsjc.Reporting.Domain.Events
{
    [EventName("Event.Reprot.CreateReportEvent")]
    public class CreateReportEvent
    {
        public Guid ReportId => Guid.NewGuid();

        public string ReportName { get; set; }

        public byte[] ReportLayout { get; set; }
    }
}
