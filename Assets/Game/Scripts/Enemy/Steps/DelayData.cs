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
        #region Public Variables

        [LabelText("延遲秒數：")] public float delayTime;

        #endregion

        #region Private Variables

        [Inject] private IFlowControl _enemyFlowControl;

        bool _isFinished = true;

        #endregion

        #region Public Methods

        public void InitAction()
        {
            _isFinished = true;
        }

        public void InjectDependencies(IFlowControl control)
        {
            _enemyFlowControl = control;
        }

        public async Task<bool> onExecute()
        {
            if (_isFinished == false)
                return false;

            if (_isFinished == true)
                _isFinished = false;

            await Task.Delay((int)(delayTime*1000));
            Debug.Log("延遲");

            return true;
        }

        public override string ToString() => "延遲";

        #endregion
    }
}