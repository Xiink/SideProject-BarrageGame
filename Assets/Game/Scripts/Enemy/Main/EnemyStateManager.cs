using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Phases;
using Game.Scripts.Enemy.States;
using Game.Scripts.Players.Main;
using ModestTree;
using UnityEngine;
using Zenject;

using UnityHFSM;

namespace Game.Scripts.Enemy.Main
{
    public class EnemyStateManager : ITickable, IFixedTickable, IInitializable
    {
        [Inject] public EnemyFollowState _enemyFollowState;

        [Inject] public EnemyData _data;

        [Inject] public IMover _enemy;

        IEnemyState _currentStateHandler;
        EnemyStates _currentState = EnemyStates.None;
        List<IEnemyState> _states;
        private EnemyPhase _currentPhase;

        private StateMachine fsm;

        public void Tick()
        {
            // if(_enemy.Moveable == false) return;
            //
            // _enemy.SetPosition(new Vector3(_enemy.GetPosition().x, _enemy.GetPosition().y, 0));
            //
            // _currentStateHandler.Update();
        }

        public void FixedTick()
        {
            // if(_enemy.Moveable == false) return;
            //
            // _currentStateHandler.FixedUpdate();
            _enemyFollowState.FixedUpdate();
            fsm.OnLogic();
        }

        public void Initialize()
        {
            // Assert.IsEqual(_currentState, EnemyStates.None);
            // Assert.IsNull(_currentStateHandler);
            //
            // ChangeState(EnemyStates.Follow);

            fsm = new StateMachine();

            fsm.AddState("Follow");

            fsm.Init();
        }

        public void ChangeState(EnemyStates state)
        {
            // if (_currentState == state)
            // {
            //     return;
            // }
            //
            // _currentState = state;
            // if (_currentStateHandler != null)
            // {
            //     _currentStateHandler.ExitState();
            //     _currentStateHandler = null;
            // }
            //
            // _currentStateHandler = _enemyFollowState;
            // _currentStateHandler.EnterState();
        }
    }
}