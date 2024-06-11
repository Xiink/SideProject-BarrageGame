using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Values;
using Game.Scripts.Values;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Steps
{
    [Serializable]
    public class EnemyStepData : DataList
    {
        public override string ToString() => "步驟資料";

        [LabelText("Step Data id List")]
        [ValueDropdown("@GetStep.steps"),HideReferenceObjectPicker]
        public List<EnemyStep> StepDataList;

        [Inject]
        private EnemyFlowControl _enemyFlowControl;

        public Task onExecute()
        {
            Debug.Log("測試");
            Debug.Log(_enemyFlowControl._enemy);
            return null;
        }

        public void InjectDependencies(IFlowControl control)
        {
            _enemyFlowControl = (EnemyFlowControl)control;
        }
    }
}