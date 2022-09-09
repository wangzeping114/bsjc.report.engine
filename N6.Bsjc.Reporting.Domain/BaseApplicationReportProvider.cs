using DevExpress.XtraReports.UI;
using N6.Bsjc.Reporting.Domain.DataSource;
using N6.Bsjc.Reporting.HttpApi.Client.Contracts.ServiceProxies;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using DevExpress.XtraReports.Parameters;
using N6.Bsjc.Reporting.Domain.Shared.Extends;
using N6.Bsjc.Reporting.Domain.Shared.ReportParameter;
using System.Collections.Generic;
using DevExpress.XtraReports.Expressions;

namespace N6.Bsjc.Reporting.Domain
{
    public abstract class BaseApplicationReportProvider
    {
        private readonly IReportServiceProxy _reportServiceProxy;
        private readonly IDataSourceFactory _dataSourceFactory;

        public BaseApplicationReportProvider(
            IReportServiceProxy reportServiceProxy, 
            IDataSourceFactory dataSourceFactory)
        {
            _reportServiceProxy=reportServiceProxy;
            _dataSourceFactory = dataSourceFactory;
        }

        public virtual async Task<XtraReport> GetReportAsync(string id)
        {
            var dto = await _reportServiceProxy.GetReportAsync(Guid.Parse(id));
            var xtraReport = GenerateXtraReport(dto.ReportLayout);
			BindReportParameter(xtraReport, dto.ReportParameterJson);
			var dataSourceProvider = _dataSourceFactory.GetDataSourceProvider(dto.ReportDataSourceType);
            var objectDataSrouce = dataSourceProvider.CreateDataSrouce(dto.ReportSourceName, dto.ReportDataSourceValue,dto.ReportDataSourceParameterJson);
            xtraReport.DataSource = objectDataSrouce;
			return xtraReport;
        }
		/// <summary>
		/// 绑定报告面板过滤参数
		/// TODO: 有待测试。
		/// </summary>
		/// <param name="xtraReport"></param>
		/// <param name="reportParameterJson"></param>
		private void BindReportParameter(XtraReport xtraReport, string reportParameterJson)
		{
			if (reportParameterJson.IsNullOrEmpty() || !reportParameterJson.IsJson())
			{
				return;
			}
			var reportParmeters = reportParameterJson.ToObject<List<ReportParmeter>>();
			reportParmeters = reportParmeters.Where(x => x.IsEnabled).ToList();
			foreach (var reportParmeter in reportParmeters)
			{
				var parameter = new Parameter();
				parameter.Name = reportParmeter.Name;
				parameter.Description = reportParmeter.Description;
				parameter.Type = reportParmeter.ValueType;
				parameter.Visible = reportParmeter.IsVisible;
				parameter.AllowNull = reportParmeter.IsAllowNull;
				parameter.SelectAllValues = reportParmeter.IsMultiValue;
				switch (reportParmeter.ReportValueSourceType)
				{
					case Shared.Emuns.ReportValueSourceType.Static:
						SettingStaticListLookUp(reportParmeter, parameter);
						break;
					case Shared.Emuns.ReportValueSourceType.Dynamic:
						SettingDynamicListLookUp(reportParmeter, parameter);
						break;
					case Shared.Emuns.ReportValueSourceType.Range:
						SettingRangeParameters(reportParmeter, parameter);
						break;
					default:
						break;
				}
				xtraReport.Parameters.Add(parameter);
				if (reportParmeter.FilterString.IsNullOrEmpty())
				{
					xtraReport.FilterString = reportParmeter.FilterString;
				}
			}
		}

		private static void SettingRangeParameters(ReportParmeter reportParmeter, Parameter parameter)
		{
			var dateRangeSettings = new RangeParametersSettings();

			if (reportParmeter.StartRangeSetting != null)
			{
				dateRangeSettings.StartParameter.Name = reportParmeter.StartRangeSetting.Name;
				dateRangeSettings.StartParameter.ExpressionBindings.Add(
					new BasicExpressionBinding(reportParmeter.StartRangeSetting.PropertyName, reportParmeter.StartRangeSetting.Expression));
			}
			if (reportParmeter.EndRangeSetting != null)
			{
				dateRangeSettings.EndParameter.Name = reportParmeter.EndRangeSetting.Name;
				dateRangeSettings.EndParameter.ExpressionBindings.Add(
					new BasicExpressionBinding(reportParmeter.EndRangeSetting.PropertyName, reportParmeter.EndRangeSetting.Expression));
			}
			parameter.ValueSourceSettings = dateRangeSettings;
		}

		private static void SettingDynamicListLookUp(ReportParmeter reportParmeter, Parameter parameter)
		{
			var dynamicListLookUpSetting = new DynamicListLookUpSettings();
			if (reportParmeter.DynamicSetting != null)
			{
				dynamicListLookUpSetting.DataMember = reportParmeter.DynamicSetting.Value;
				dynamicListLookUpSetting.DisplayMember = reportParmeter.DynamicSetting.Description;
				parameter.ValueSourceSettings = dynamicListLookUpSetting;
			}
		}

		private static void SettingStaticListLookUp(ReportParmeter reportParmeter, Parameter parameter)
		{
			var staticListLookUpSettings = new StaticListLookUpSettings();
			if (reportParmeter.StaticSettings != null && reportParmeter.StaticSettings.Any())
			{
				foreach (var item in reportParmeter.StaticSettings)
				{
					staticListLookUpSettings.LookUpValues.Add(new LookUpValue
					{
						Value = item.Value,
						Description = item.Description
					});
				}
				parameter.ValueSourceSettings = staticListLookUpSettings;
			}
		}

		private static XtraReport GenerateXtraReport(byte[] reportLayoutBytes)
        {
            if (reportLayoutBytes == null || reportLayoutBytes.Length == 0)
            {
                return new XtraReport();
            }
            using (var ms = new MemoryStream(reportLayoutBytes))
            {
                var report = XtraReport.FromXmlStream(ms);
                return report;
            }
        }
    }
}
