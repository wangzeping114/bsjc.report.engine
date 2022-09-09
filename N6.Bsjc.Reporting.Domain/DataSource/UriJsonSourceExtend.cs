using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.DataAccess;
using DevExpress.DataAccess.Json;
using N6.Bsjc.Reporting.Domain.Shared.Extends;
using N6.Bsjc.Reporting.Domain.Shared.SourceParameters;

namespace N6.Bsjc.Reporting.Domain.DataSource
{
	public static class UriJsonSourceExtend
	{
		public static void BuildParameters(this UriJsonSource uriJsonSource, string parameterJson)
		{
			if (uriJsonSource == null)
			{
				return;
			}
			if (parameterJson.IsNullOrEmpty())
			{
				return;
			}
			if (!parameterJson.IsJson())
			{
				return;
			}

			var parmamters = parameterJson.ToObject<List<UriParameter>>();
			var needParmamters = parmamters.Where(x => x.IsRequired).ToList();
			foreach (var parmamter in needParmamters)
			{
				switch (parmamter.UriParameterType)
				{
					case Shared.Emuns.UriParameterType.Query:
						if (parmamter.IsExpression)
						{
							uriJsonSource.QueryParameters.Add(new QueryParameter
							{
								Name = parmamter.Name,
								Type = typeof(Expression),
								Value = new Expression($"?{parmamter.Name}")
							});
						}
						else
						{
							uriJsonSource.QueryParameters.Add(new QueryParameter
							{
								Name = parmamter.Name,
								Type = parmamter.ValueType,
								Value = parmamter.DefaultValue
							});
						}
						break;
					case Shared.Emuns.UriParameterType.Path:
						if (parmamter.IsExpression)
						{
							uriJsonSource.PathParameters.Add(new PathParameter
							{
								Name = parmamter.Name,
								Type = typeof(Expression),
								Value = new Expression($"?{parmamter.Name}")
							});
						}
						else
						{
							uriJsonSource.PathParameters.Add(new PathParameter
							{
								Name = parmamter.Name,
								Type = parmamter.ValueType,
								Value = parmamter.DefaultValue
							});
						}
						break;
					case Shared.Emuns.UriParameterType.Header:
						if (parmamter.IsExpression)
						{
							uriJsonSource.HeaderParameters.Add(new HeaderParameter
							{
								Name = parmamter.Name,
								Type = typeof(Expression),
								Value = new Expression($"?{parmamter.Name}")
							});
						}
						else
						{
							uriJsonSource.HeaderParameters.Add(new HeaderParameter
							{
								Name = parmamter.Name,
								Type = parmamter.ValueType,
								Value = parmamter.DefaultValue
							});
						}
						break;
					default:
						break;
				}
			}
		}
	}
}
