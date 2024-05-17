using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyDataContainer", menuName = "EnemyDataContainer",order = 0)]
    public class EnemyData : ScriptableObjectInstaller
    {
        [BoxGroup("Domain Data")]
        [LabelWidth(100)]
        public float Life;
        [BoxGroup("Domain Data")]
        [LabelWidth(100)]
        public float MP;
        [BoxGroup("Domain Data")]
        [LabelWidth(100)]
        public float Offset;

        [LabelText("Size")]
        [HorizontalGroup("Domain Data/Group 1", LabelWidth = 20)]
        public Label Size ;
        [HorizontalGroup("Domain Data/Group 1")]
        public float x;
        [HorizontalGroup("Domain Data/Group 1")]
        public float y;
        // [BoxGroup("Domain Data")]


        [SerializeField]
        private Enemy.Data _data;

        public override void InstallBindings()
        {
            Container.BindInstance(_data);
        }

        // [Serializable]
        public class TestOffset
        {

        }
    }
}