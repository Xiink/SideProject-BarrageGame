using System.Collections.Generic;
using System.Threading;
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
        [Inject]
        public EnemyFollowState _enemyFollowState;

        [Inject]
        public EnemyEnterState _enemyEnterState;

        // [Inject] public EnemyData _data;

        // [Inject] public IMover _enemy;

        IEnemyState _currentStateHandler;
        // EnemyStates _currentState = EnemyStates.None;
        // List<IEnemyState> _states;
        // private EnemyPhase _currentPhase;

        private StateMachine fsm;
        //
        // private string _currentStateName = string.Empty;
        // private string _newStateName = string.Empty;

        // private bool changeState = false;

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
            // _enemyFollowState.FixedUpdate();
            fsm.OnLogic();
            _currentStateHandler.FixedUpdate();
        }

        public void Initialize()
        {
            // Assert.IsEqual(_currentState, EnemyStates.None);
            // Assert.IsNull(_currentStateHandler);
            //
            // ChangeState(EnemyStates.Follow);

            _currentStateHandler = _enemyEnterState;

            fsm = new StateMachine();

            // fsm.AddState("ExtractIntel");
            fsm.AddState(TestStatNames.Follow, new State(_state => _currentStateHandler.EnterState()));
            fsm.AddState(TestStatNames.Loop, new State(_state => _currentStateHandler.EnterState()));

            fsm.AddTransition(new TransitionAfter("Loop", "Follow", 10));

            // fsm.AddState(TestStatNames.Wait, new State(_state => Debug.Log("Wait")));
            // fsm.AddTriggerTransition(
            //     "OnCollision",
            //     new Transition(TestStatNames.Follow, TestStatNames.Loop)
            // );
            fsm.SetStartState(TestStatNames.Loop);
            // _currentStateName = TestStatNames.Follow;
            fsm.Init();

            // Thread.Sleep(5000);
            // ChangeState(TestStatNames.Loop);
        }

        public void ChangeState(string _newStateName)
        {
            // fsm.AddTransition(_currentStateName, _newStateName);
            // _currentStateName = _newStateName;
            // this._newStateName = this._newStateName;
            // fsm.Trigger("OnCollision");
            // _currentStateName = _newStateName;
        }

        public void ChangeState(EnemyStates state)
        {
            //TODO : StateName?
            //TODO :  fsm.AddState("StateName.ExtractIntel");

            // fsm.AddTwoWayTransition("ExtractIntel", "ChangeState",
            //     transition => changeState == true);
        }
    }

    public class TestStatNames
    {
        public const string Follow = "Follow";
        public const string Loop = "Loop";
        public const string Wait = "Wait";
    }
}