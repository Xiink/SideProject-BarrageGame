using Game.Scripts.Enemy.Steps;
using Game.Scripts.Names;
using Sirenix.OdinInspector;

namespace Game.Scripts.Values
{
    public class DropDownList
    {
        public static ValueDropdownList<string> GetBaseStatNames = new ValueDropdownList<string>()
        {
            {"基礎數值 / 移動速度" , StatNames.MoveSpeed},
            {"基礎數值 / 攻擊力" , StatNames.Atk},
            {"基礎數值 / 生命值" , StatNames.Hp},
        };

        public static ValueDropdownList<string> GetAllStatNames => GetBaseStatNames;

        public static ValueDropdownList<DataList> GetStructureNames = new ValueDropdownList<DataList>()
        {
            // {"階段資料",},
            // {"序列資料",},
            {"步驟資料",new EnemyStepData()},
            // {"動作資料",},
            {"延遲",new DelayData()},

        };

        public static ValueDropdownList<DataList> GetAllStructureNames => GetStructureNames;
    }
}