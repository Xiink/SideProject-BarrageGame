using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Handlers
{
    public class EnemyMoveHandler : ITickable
    {
        [Inject]
        private IMover _enemy;

        [Inject]
        public EnemyData _data;

        [Inject]
        private ITimeProvider _timeProvider;

        public void Tick()
        {
            Debug.Log(_data._domaindata.speed);
        }

        public void Move(Vector2 MoveDirection)
        {
            if(_enemy.Moveable == false) return;

            var moveSpeed = _data._domaindata.speed;
            var movement = _timeProvider.GetDeltaTime() * moveSpeed * MoveDirection;
            var newPos = movement + _enemy.GetPosition();

            _enemy.SetPosition(newPos);
        }

    }
}