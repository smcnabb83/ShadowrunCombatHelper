using System;

namespace ShadowrunCombatHelper.Globals
{
    public static class UtilityFunctions
    {
        public static T ClampLower<T>(this T val, T min) where T : IComparable<T>
        {
            return val.CompareTo(min) < 0 ? min : val;
        }

        public static T ClampUpper<T>(this T val, T max) where T : IComparable<T>
        {
            return val.CompareTo(max) > 0 ? max : val;
        }

        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            return val.ClampLower(min).ClampUpper(max);
        }
    }
}