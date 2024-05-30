using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Phases
{
    [CreateAssetMenu(fileName = "EnemyPhases", menuName = "EnemyPhasesContainer",order = 0)]
    public class Phase : ScriptableObjectInstaller
    {
        [LabelText("顯示名稱：")]
        public string DesplayName;

        [LabelText("唯一ID：")]
        public uint uid;
    }
}