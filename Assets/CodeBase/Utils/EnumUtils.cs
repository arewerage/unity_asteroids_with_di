using System;
using System.Linq;

namespace CodeBase.Utils
{
    public class EnumUtils
    {
        public static T GetRandomEnumValue<T>() where T : Enum =>
            (T)Enum.GetValues(typeof(T))
                .OfType<Enum>()
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();
    }
}
