using System;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.Test.Tests.Dto
{
	public class ReportDefineTestDto
	{
		public Guid Id { get; set; }

		public ReportDataSourceType ReportDataSourceType { get; set; }

		public string ReportDataSourceValue { get; set; }

		public string ReportParameterJson { get; set; }


		public byte[] ReportLayout { get; set; }

		public string ReportTitle { get; set; }

		public string ReportSourceName { get; set; }
	}
}
