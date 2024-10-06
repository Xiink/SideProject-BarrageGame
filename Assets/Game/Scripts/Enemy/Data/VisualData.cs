using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyVisualData", menuName = "EnemyVisualData", order = 0)]
    //[Serializable]
    public class VisualData : ScriptableObject
    {
        public string DisplayName;
        public Material EnemyMaterial;

        [VerticalGroup("base")]
        [PreviewField(ObjectFieldAlignment.Left,Height = 150)]
        [HideLabel]
        public Sprite _Sprite;
    }
}

