using System.Reflection;
using System.Runtime.Serialization;

namespace CoWorkingProject.Server.Services
{
	public class EnumService
	{
		public static string GetEnumMemberValue(Enum enumValue)
		{
			var member = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
			return member?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? enumValue.ToString();
		}

		public static TEnum? GetEnumFromEnumMemberValue<TEnum>(string value) where TEnum : struct, Enum
		{
			foreach (var field in typeof(TEnum).GetFields())
			{
				var attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) as EnumMemberAttribute;
				if (attribute != null && attribute.Value?.Equals(value, StringComparison.OrdinalIgnoreCase) == true)
				{
					return (TEnum)field.GetValue(null)!;
				}
			}
			return null;
		}
	}
}
