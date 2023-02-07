using System;

namespace CodeBase.Utils
{
    public static class EnumExtensions
    {
        public static bool TryGetNext<T>(this T myEnum, out T nextValue) where T : Enum
        {
            var array = (T[])Enum.GetValues(myEnum.GetType());
            int index = Array.IndexOf(array, myEnum) + 1;
            
            nextValue = array[index];
            
            return array.Length < index;
        }
    }
}