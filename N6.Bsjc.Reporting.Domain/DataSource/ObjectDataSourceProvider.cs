using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess.ObjectBinding;
using N6.Bsjc.Reporting.Domain.Shared.Emuns;
using N6.Bsjc.Reporting.Domain.Shared.Extends;
using N6.Bsjc.Reporting.Domain.Shared.SourceParameters;
using Newtonsoft.Json;
namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public class ObjectDataSourceProvider : IDataSourceProvider
	{
		public ReportDataSourceType ReportDataSourceType => ReportDataSourceType.Object;

		/// <summary>
		/// 绑定对象数据源, 仅支持默认构造函数的数据源
		/// </summary>
		/// <param name="name"></param>
		/// <param name="dataSrouce"></param>
		/// <param name="parameterJson"></param>
		/// <returns></returns>
		public object CreateDataSrouce(string name, string dataSrouce,string parameterJson="")
		{
			var dataSource = new ObjectDataSource();
			dataSource.Name = name;
			var obj = JsonConvert.DeserializeObject(dataSrouce);
			dataSource.DataSource = obj;
 			dataSource.Constructor = ObjectConstructorInfo.Default;
			BuildParameters(dataSource, parameterJson);
			return dataSource;
		}

		private void BuildParameters(ObjectDataSource objectDataSource, string parameterJson) 
		{
			if (!parameterJson.IsNullOrEmpty()&& parameterJson.IsJson())
			{
				var objectParameter = parameterJson.ToObject<ObjectParameter>();
				objectDataSource.DataMember = objectParameter.DataMember;
				if (objectParameter.Parameters != null)
				{
					var needParameters = objectParameter.Parameters.Where(x => x.IsRequired).ToList();
					foreach (var parameter in needParameters)
					{
						if (parameter.IsRequired)
						{
							objectDataSource.Parameters.Add(new DevExpress.DataAccess.ObjectBinding.Parameter
							{
								Name = parameter.Name,
								Type = typeof(DevExpress.DataAccess.Expression),
								Value = new DevExpress.DataAccess.Expression($"?{parameter.Name}")
							});
						}
						else
						{
							objectDataSource.Parameters.Add(new DevExpress.DataAccess.ObjectBinding.Parameter
							{
								Name = parameter.Name,
								Type = parameter.ValueType,
								Value = parameter.DefaultValue
							});
						}

					}
				}
			}
		}
	}
}
