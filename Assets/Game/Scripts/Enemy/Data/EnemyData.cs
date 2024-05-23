using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Game.Scripts.Enemy.Values;

namespace Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyDataContainer", menuName = "EnemyDataContainer",order = 0)]
    public class EnemyData : ScriptableObjectInstaller
    {
        // [HorizontalGroup("Split")]
        // [FoldoutGroup("Split/Domain Data")]
        // [LabelWidth(100)]
        // public float Life;
        // [FoldoutGroup("Split/Domain Data")]
        // [LabelWidth(100)]
        // public float MP;
        // [FoldoutGroup("Split/Domain Data")]
        // [LabelWidth(100)]
        // public float Offset;
        //
        // [LabelText("Size")]
        // [HorizontalGroup("Split/Domain Data/Group 1", LabelWidth = 20)]
        // public Label Size ;
        // [HorizontalGroup("Split/Domain Data/Group 1")]
        // public float x;
        // [HorizontalGroup("Split/Domain Data/Group 1")]
        // public float y;
        //
        // [FoldoutGroup("Split/Visual Data")]
        // public string DisplayName;
        // [FoldoutGroup("Split/Visual Data")]
        // public Material EnemyMaterial;
        //
        //
        // [FoldoutGroup("Behaviour")]
        // [ValueDropdown("@GetAllPhases.GetAllPhaseNames", ExpandAllMenuItems = true,
        //     IsUniqueList = true,
        //     DropdownHeight = 250, DropdownWidth = 300)]
        // public string Behaviour;

        [SerializeField]
        public Enemy.Data _data;

        public int value = 0;

        public override void InstallBindings()
        {
            Container.BindInstance(this).AsSingle();
        }
    }
}