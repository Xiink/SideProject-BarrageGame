using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Phases;
using Game.Scripts.Enemy.Steps;
using Game.Scripts.Enemy.Values;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy
{
    public class EnemyFlowControl : ITickable,IInitializable,IFlowControl
    {
        public EnemyPhase _currentPhase;
        public EnemyStep _currentStep;

        public DataList _currentAction;

        [Inject]
        public EnemyData _data;

        [Inject]
        public IMover _enemy;

        private bool _finished = true; // 用來確認Action是否完成
        private int currentIndex = 0; // 用來確認目前在執行哪個Action

        public async void Tick()
        {
            // 20240813暫時註解
            // var result = await _currentAction.onExecute();
            // if (result == true)
            // {
            //     currentIndex++;
            //     if (currentIndex >= _data.Behaviour.PhaseDataList.Count)
            //         currentIndex = 0;
            //     ChangeAction(currentIndex);
            // }
        }

        /// <summary>
        /// 切換要進行的Action
        /// </summary>
        /// <param name="index"></param>
        public void ChangeAction(int index)
        {
            // 如果不是第一次，則將已經完成的Action進行初始化
            if(_currentAction != null)
                _currentAction.InitAction();

            var actions = _data.Behaviour.PhaseDataList;
            _currentAction = actions[index].Data;

            _finished = true;
        }

        public async void Initialize()
        {
            var phase = _data.Behaviour;
            var actions = _data.Behaviour.PhaseDataList;

            // 手動綁定
            // 需要進行省略，要研究怎麼處理
            foreach(var action in actions)
            {
                try
                {
                    action.Data.InjectDependencies(this);
                }
                catch
                {

                }
            }

            currentIndex = 0;
            ChangeAction(currentIndex);
        }
    }
}