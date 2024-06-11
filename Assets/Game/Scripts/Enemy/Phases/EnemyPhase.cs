using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts.Enemy.Phases
{
    [CreateAssetMenu(fileName = "EnemyPhases", menuName = "EnemyPhasesContainer",order = 0)]
    public class EnemyPhase : ScriptableObjectInstaller
    {
        [FormerlySerializedAs("DesplayName")] [LabelText("顯示名稱：")]
        public string DisplayName;

        [LabelText("唯一ID：")]
        public uint uid;

        public List<EnemyPhaseData> PhaseDataList;
    }

    public static class GetPhases
    {
        public static ValueDropdownList<EnemyPhase> phases => Get();

        public static ValueDropdownList<EnemyPhase> Get()
        {
            ValueDropdownList<EnemyPhase> result = new ValueDropdownList<EnemyPhase>();
            var allAssets = AssetUtilities.GetAllAssetsOfType(typeof(EnemyPhase), "Assets/Game/Datas/EnemyDatas/Phase");
            var phases = allAssets.GetEnumerator();

            List<EnemyPhase> phaselist = new List<EnemyPhase>();

            while (phases.MoveNext())
            {
                var phase = (EnemyPhase)phases.Current;
                phaselist.Add(phase);
            }

            phaselist = phaselist.OrderBy(phase => phase.uid).ToList();

            foreach (var phase in phaselist)
            {
                var newPhase = new ValueDropdownList<EnemyPhase>(){{phase.DisplayName,phase}};
                result.AddRange(newPhase);
            }

            return result;
        }
    }
}