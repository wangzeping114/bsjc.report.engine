using System;
using System.Collections.Generic;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;

namespace N6.Bsjc.Reporting.Domain.Shared.ReportParameter
{
	public class ReportParmeter
	{
		/// <summary>
		/// 显示名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 数据类型 <see cref="https://docs.devexpress.com/XtraReports/9997/detailed-guide-to-devexpress-reporting/use-report-parameters/create-a-report-parameter#type"/>
		/// </summary>
		public Type ValueType { get; set; }

		/// <summary>
		/// 是否可见
		/// </summary>
		public bool IsVisible { get; set; } = true;

		/// <summary>
		/// 是否启用
		/// </summary>
		public bool IsEnabled { get; set; } = true;

		/// <summary>
		/// 是否为空
		/// </summary>
		public bool IsAllowNull { get; set; } = true;

		/// <summary>
		/// 是否支持多选
		/// </summary>
		public bool IsMultiValue { get; set; } = true;

		/// <summary>
		/// 描述
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 过滤字段
		/// </summary>
		public string FilterString { get; set; }

		/// <summary>
		/// 源值类型
		/// </summary>
		public ReportValueSourceType ReportValueSourceType { get; set; }


		/// <summary>
		/// 对于参数的默认值，只能在表达式中使用常量、操作符、日期-时间、逻辑、数学和字符串函数
		/// 有关表达式的语法<see cref="https://docs.devexpress.com/XtraReports/120104/detailed-guide-to-devexpress-reporting/use-expressions/expression-language#constants"/>
		/// </summary>
		public List<Setting> StaticSettings { get; set; }

		/// <summary>
		/// 动态表达式
		/// </summary>
		public Setting DynamicSetting { get; set; }

		/// <summary>
		/// 开始区域表达式设置
		/// </summary>
		public RangeSetting StartRangeSetting { get; set; }

		/// <summary>
		/// 结束区域表达式设置
		/// </summary>
		public RangeSetting EndRangeSetting { get; set; }

	}

	public class Setting 
	{
		public Setting(string value,string description)
		{
			Value = value;
			Description = description;
		}

		public string Value { get; set; }

		public string Description { get; set; }
	}

	public class RangeSetting 
	{
		public string Name { get; set; }

		public string PropertyName { get; set; }

		public  string Expression { get; set; }
	}

}
