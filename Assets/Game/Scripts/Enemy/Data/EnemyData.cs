using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Game.Scripts.Enemy.Values;
using Assets.Game.Scripts.Enemy.Data;

namespace Game.Scripts.Enemy.Data
{
    [CreateAssetMenu(fileName = "EnemyDataContainer", menuName = "EnemyDataContainer",order = 0)]
    public class EnemyData : ScriptableObjectInstaller
    {
        [HorizontalGroup("Split")]
        [FoldoutGroup("Split/Domain Data")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [HideLabel]
        public DomainData _domaindata;


        [HorizontalGroup("Split")]
        [FoldoutGroup("Split/Visual Data")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [HideLabel]
        public VisualData _visualData;

        [FoldoutGroup("Behaviour")]
        [ValueDropdown("@GetAllPhases.GetAllPhaseNames", ExpandAllMenuItems = true,
        IsUniqueList = true,
        DropdownHeight = 250, DropdownWidth = 300)]
        public string Behaviour;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}