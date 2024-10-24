using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Game.Scripts.RPG;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyDomainData", menuName = "EnemyDomainData", order = 0)]
    //[Serializable]
    public class DomainData : ScriptableObject
    {
        public List<Stat.Data> Datas;

        // [FormerlySerializedAs("Life")]
        // [LabelWidth(100)]
        // public float life;
        // [FormerlySerializedAs("MP")]
        // [LabelWidth(100)]
        // public float mp;
        [LabelWidth(100)]
        public float speed;
        //
        //
        // [HorizontalGroup("Group 1", LabelWidth = 100)]
        // [LabelText("BoxColliderSize")]
        // public Label Size;
        // [HorizontalGroup("Group 1")]
        // [LabelWidth(30)]
        // public float x;
        // [HorizontalGroup("Group 1")]
        // [LabelWidth(30)]
        // public float y;
    }
}
