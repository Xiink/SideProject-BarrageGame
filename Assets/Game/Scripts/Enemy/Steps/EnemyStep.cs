using System.Collections.Generic;
using Game.Scripts.Values;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Game.Scripts.Enemy.Steps
{
    [CreateAssetMenu(fileName = "EnemyStep", menuName = "EnemyStepContainer",order = 0)]
    public class EnemyStep : ScriptableObject,IStep
    {

    }

    public static class GetStep
    {
        public static ValueDropdownList<EnemyStep> steps => Get();

        public static ValueDropdownList<EnemyStep> Get()
        {
            ValueDropdownList<EnemyStep> result = new ValueDropdownList<EnemyStep>();
            var allAssets = AssetUtilities.GetAllAssetsOfType(typeof(EnemyStep), "Assets/Game/Datas/EnemyDatas/Steps");
            var steps = allAssets.GetEnumerator();

            List<EnemyStep> steplist = new List<EnemyStep>();

            while (steps.MoveNext())
            {
                var phase = (EnemyStep)steps.Current;
                steplist.Add(phase);
            }

            // steplist = steplist.OrderBy(phase => phase.uid).ToList();

            foreach (var step in steplist)
            {
                var newstep = new ValueDropdownList<EnemyStep>(){{step}};
                result.AddRange(newstep);
            }

            return result;
        }
    }
}