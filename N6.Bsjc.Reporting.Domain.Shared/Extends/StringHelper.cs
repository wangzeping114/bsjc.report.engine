using Newtonsoft.Json.Linq;

namespace N6.Bsjc.Reporting.Domain.Shared.Extends
{
	public static class StringHelper
	{
		public static bool IsJson(this string str) 
		{
			if (string.IsNullOrWhiteSpace(str))
			{
				return false;
			}
			str = str.Trim();
			if ((str.StartsWith("{") && str.EndsWith("}")) || //For object
				(str.StartsWith("[") && str.EndsWith("]"))) //For array
			{
				try
				{
					var obj = JToken.Parse(str);
					return true;
				}
				catch
				{
					return false;
				}
			}
			return false;
		}
	}
}
