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
        };

        public static ValueDropdownList<string> GetAllStatNames => GetBaseStatNames;

    }
}