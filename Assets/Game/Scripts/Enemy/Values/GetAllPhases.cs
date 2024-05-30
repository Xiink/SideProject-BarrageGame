using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Enemy.Phases;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Game.Scripts.Enemy.Values
{
    public class GetAllPhases
    {
        public static ValueDropdownList<string> result = new ValueDropdownList<string>();

        public static ValueDropdownList<string> GetAllPhaseNames => Get();

        public static ValueDropdownList<string> Get()
        {
            result.Clear();
            var allAssets = AssetUtilities.GetAllAssetsOfType(typeof(Phase), "Assets/Game/Datas/EnemyDatas/Phase");
            var phases = allAssets.GetEnumerator();

            while (phases.MoveNext())
            {
                var phase = (Phase)phases.Current;
                result.Add(phase.DesplayName);
            }

            return result;
        }
    }
}