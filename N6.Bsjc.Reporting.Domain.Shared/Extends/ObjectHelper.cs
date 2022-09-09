using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace N6.Bsjc.Reporting.Domain.Shared.Extends
{
	public static class ObjectHelper
	{
		public static TSource Clone<TDestination, TSource>(this TDestination destination)
			where TDestination : class
			where TSource : class
		{
			return destination.ToJson().ToObject<TSource>();
		}
	}
}
