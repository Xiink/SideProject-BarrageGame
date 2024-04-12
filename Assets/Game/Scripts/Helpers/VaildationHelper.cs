using System.Collections.Generic;
using System.Linq;
using Game.Scripts.RPG;
using rStarUtility.Util.Extensions.Csharp;

namespace Game.Scripts.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateString(string str)
        {
            return str.HasValue();
        }


        /// <summary>
        /// 驗證是否有重複資料
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static bool StatDataValidation(List<Stat.Data> datas, ref string errorMessage)
        {
            var groupByName = datas.GroupBy(data => data.Name);
            var anyDuplicateName = groupByName.Any(g => g.Count() > 1);
            var normalCase = anyDuplicateName == false;
            if (normalCase) return true;
            var DuplicateName = groupByName.First(g => g.Count() > 1);
            errorMessage = $"Find Duplicate Data :  {DuplicateName.Key}";
            return false;
        }
    }
}