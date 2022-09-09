using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace N6.Bsjc.Reporting.Domain.Shared.Extends
{
	public static class SerializeHelper
	{
		public static string ToJson<T>(this T obj) where T : class
		{
			var stringAsJson = JsonConvert.SerializeObject(obj);
			return stringAsJson;
		}

		public static T ToObject<T>(this string stringAsJson) where T : class
		{
			return JsonConvert.DeserializeObject<T>(stringAsJson, new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				NullValueHandling = NullValueHandling.Ignore,
			});
		}
		
	}
}
