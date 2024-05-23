using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Values
{
    [CreateAssetMenu(fileName = "EnemyPhases", menuName = "EnemyPhasesContainer",order = 0)]
    public class Phases : ScriptableObjectInstaller
    {
        [LabelText("顯示名稱：")]
        public string DesplayName;

        [LabelText("唯一ID：")]
        public uint uid;

        // [OdinSerialize]
        // public List<string> GetPhases = new List<string>()
        // {
        //     {"第一階段" },
        //     {"第二階段"},
        // };

        // public List<string> GetAllPhases => GetPhases;
    }
}