using System;
using System.Threading;
using System.Threading.Tasks;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.States;
using Game.Scripts.RPG;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Main
{
    public class EnemyStateManager : ITickable,IInitializable
    {
        [Inject] private IState _currentStateHandler;

        private EnemyStates _currentState = EnemyStates.None;

        public void Tick()
        {
            _currentStateHandler.OnUpdate();
        }

        public void ChangeState(EnemyStates states)
        {

        }

        public void Initialize()
        {
            _currentStateHandler.OnEnter();
        }
    }
}