using System;
using System.Collections.Generic;
using Game.Scripts.Names;
using rStarUtility.Util.Extensions.Csharp;

namespace Game.Scripts.Values
{
    public class StatMinMaxValues
    {
        /// <summary>
        /// stat`s name , max amount
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, float> maxLookup = new Dictionary<string, float>()
        {
            {StatNames.MoveSpeed,30},
            {StatNames.Atk,1000},
        };

        /// <summary>
        /// stat`s name , min amount
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, float> minLookup = new Dictionary<string, float>()
        {
            {StatNames.MoveSpeed,1},
            {StatNames.Atk,0}
        };

        public static float GetMax(string name)
        {
            return maxLookup.GetOrReturn(name, float.MaxValue);
        }

        public static float GetMin(string name)
        {
            return minLookup.GetOrReturn(name, 0);
        }

        public static string GEtMaxInfo(string name)
        {
            var max = GetMax(name);
            return Math.Abs(max - float.MaxValue) < 0.01f ? "Float.Max" : max.ToString();
        }

        public static string GEtMinInfo(string name,float MinValue)
        {
            var min = GetMin(name);
            return Math.Abs(min - MinValue) < 0.01f ? MinValue.ToString() : min.ToString();
        }
    }
}