using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Assets.Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Phases;
using Game.Scripts.RPG;

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
        [ValueDropdown("@GetPhases.phases", ExpandAllMenuItems = true,
            IsUniqueList = true,
            DropdownHeight = 250, DropdownWidth = 300)]
        public EnemyPhase Behaviour;

        public float maxhp = 10;
        public Stat.Data hp;

        public override void InstallBindings()
        {
            Container.BindInstance(this);
        }
    }
}