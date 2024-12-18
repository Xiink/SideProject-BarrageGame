using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Names;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Handlers
{
    public class EnemyMoveHandler : ITickable
    {
        [Inject]
        private IMover _enemy;

        // [Inject]
        // public EnemyData _data;

        [Inject]
        private ITimeProvider _timeProvider;

        public void Tick()
        {
            // Debug.Log(_enemy + $"{_data._domaindata.life}");
        }

        public void Move(Vector2 MoveDirection)
        {
            if(_enemy.Moveable == false) return;

            var moveSpeed = _enemy.GetStatFinalValue(StatNames.MoveSpeed);
            var movement = _timeProvider.GetDeltaTime() * moveSpeed * MoveDirection;
            var newPos = movement + _enemy.GetPosition();

            _enemy.SetPosition(newPos);
        }

        public void AddForce()
        {
            if(_enemy.Moveable == false) return;

            var playerDir = new Vector2(10,0).normalized;

            _enemy.AddForce(playerDir * 15);

            Debug.Log(_enemy.GetPosition());
        }
    }
}