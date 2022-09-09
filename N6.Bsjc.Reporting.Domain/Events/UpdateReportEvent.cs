using System;
using Volo.Abp.EventBus;

namespace N6.Bsjc.Reporting.Domain.Events
{
    [EventName("Event.Reprot.UpdateReportEvent")]
    public class UpdateReportEvent
    {
        public Guid ReportId { get; set; }

        public byte[] ReportLayout { get; set; }

        public string ReportName { get; set; }
    }
}
