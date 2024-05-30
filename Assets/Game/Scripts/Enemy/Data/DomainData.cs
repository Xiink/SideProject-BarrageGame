using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyDomainData", menuName = "EnemyDomainData", order = 0)]
    //[Serializable]
    public class DomainData : ScriptableObject
    {
        public float Life;
        [LabelWidth(100)]
        public float MP;
        [LabelWidth(100)]
        public float Offset;

        [LabelText("Size")]
        [HorizontalGroup("Group 1", LabelWidth = 20)]
        public Label Size;
        [HorizontalGroup("Group 1")]
        public float x;
        [HorizontalGroup("Group 1")]
        public float y;
    }
}
