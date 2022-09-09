using System;
using System.Collections.Generic;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;
using N6.Bsjc.Reporting.Domain.Shared.Extends;
using N6.Bsjc.Reporting.Domain.Shared.ReportParameter;

namespace N6.Bsjc.Reporting.Domain.Shared
{
	public class ReportDefineModel
	{
		public Guid Id { get; set; }

		public ReportDataSourceType ReportDataSourceType { get; set; }

		public string ReportDataSourceValue { get; set; }

		public string ReportDataSourceParameterJson { get; set; }

		public byte[] ReportLayout { get; set; }

		public string ReportTitle { get; set; }

		public string ReportSourceName { get; set; }

		public string ReportParameterJson { get; set; } // = ReportParameterTest();

		/// <summary>
		/// 入参测试
		/// </summary>
		/// <returns></returns>
		public static string ReportParameterTest()
		{
			var reportParmeters = new List<ReportParmeter>();
			reportParmeters.Add(new ReportParmeter
			{
				Name = "lastTestDate1",
				Description = "lastTestDate1",
				IsVisible=false,
				ReportValueSourceType = ReportValueSourceType.Static,
				ValueType = typeof(DateTime),
				StaticSettings = new List<Setting>
				{
					new Setting(new DateTime(2022, 01, 01).ToString(),"January 1, 2014"),
					new Setting(new DateTime(2021, 08, 24).ToString(),"February 08, 2021"),
					new Setting(new DateTime(2022, 08, 25).ToString(),"February 08, 2022"),
				},
				FilterString = $"GetDate([lastTestDate]) >= ?lastTestDate"
			});
			return reportParmeters.ToJson();
		}
	}
}
