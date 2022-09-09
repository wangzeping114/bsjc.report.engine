using System;
using Newtonsoft.Json;

namespace N6.Bsjc.Reporting.Domain.Shared.SourceParameters
{
	public class Parameter
	{
		public bool IsExpression { get; set; } = false;

		[JsonIgnore]
		public Type ValueType { get; set; } = typeof(string);

		public bool IsRequired { get; set; } = true;

		public string DefaultValue { get; set; }

		public string Name { get; set; }
	}
}
