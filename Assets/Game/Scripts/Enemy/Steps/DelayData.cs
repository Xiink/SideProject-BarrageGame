using System;
using System.Collections;
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
    public class DelayData : DataList
    {
        public override string ToString() => "延遲";

        [LabelText("延遲秒數：")]
        public float delayTime;

        [Inject]
        private IFlowControl _enemyFlowControl;

        public async Task onExecute()
        {
            await Task.Delay(5000);
            Debug.Log("延遲");
        }

        public void InjectDependencies(IFlowControl control)
        {
            _enemyFlowControl = control;
        }
    }
}