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
	}
}
