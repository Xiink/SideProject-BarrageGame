using System;
using System.Collections.Generic;
using Game.Scripts.Enemy.Steps;
using Game.Scripts.Values;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Enemy.Phases
{
    [Serializable]
    public class EnemyPhaseData
    {
        [ValueDropdown("@DropDownList.GetAllStructureNames"),HideReferenceObjectPicker]
        [SerializeReference]
        [HideLabel]
        public DataList Data;
    }
}