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

        private bool _finished = true;
        private int currentIndex = 0;

        public async void Tick()
        {
            // var nowAction = _data.Behaviour.PhaseDataList[0].Data;
            //
            // System.Reflection.MemberInfo info = typeof(DelayData);
            //
            // if (nowAction.GetType() == typeof(DelayData))
            // {
            //     await Task.Delay(5000);
            //     Debug.Log("Delay");
            // }
        }

        public async void Initialize()
        {
            var phase = _data.Behaviour;
            var actions = _data.Behaviour.PhaseDataList;

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
            // while (true)
            // {
            //     foreach(var action in actions)
            //     {
            //         try
            //         {
            //             await action.Data.onExecute();
            //         }
            //         catch
            //         {
            //
            //         }
            //     }
            // }
        }
    }
}