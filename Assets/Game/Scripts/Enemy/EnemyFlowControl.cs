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
    public class EnemyFlowControl : IInitializable,IFlowControl
    {
        public EnemyPhase _currentPhase;
        public EnemyStep _currentStep;

        [Inject]
        public EnemyData _data;

        [Inject]
        public IMover _enemy;

        private bool _finished = true; // 用來確認Action是否完成
        private int currentIndex = 0; // 用來確認目前在執行哪個Action

        public async void Tick()
        {

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

            // 依序進行執行Action
            foreach(var action in actions)
            {
                try
                {
                    await action.Data.onExecute();
                }
                catch
                {

                }
            }
        }
    }
}