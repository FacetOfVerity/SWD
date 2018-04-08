using System;
using System.Reflection;

namespace SWD.Utils.Docx.Extensions
{
    public static class MemberInfoExtensions
    {
        public static TValue GetValue<TValue>(this MemberInfo info, object data) where TValue : class, IConvertible
        {
            TValue value = null;

            var prop = info as PropertyInfo;
            if (prop != null)
            {
                value = (TValue)Convert.ChangeType(prop.GetValue(data), typeof(TValue));
            }

            var field = info as FieldInfo;
            if (field != null)
            {
                value = (TValue)Convert.ChangeType(field.GetValue(data), typeof(TValue));
            }

            return value;
        }
    }
}
