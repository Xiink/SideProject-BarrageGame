using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Main;
using Game.Scripts.Players.Handlers;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.States
{
    public class EnemyFollowState : IEnemyState
    {
        [Inject]
        private IMover _enemy;
        // private EnemyStateManager _enemyStateManager;

        /// <summary>
        /// 抓取PlayerCharacter的Imover
        /// </summary>
        [Inject(Id = "PlayerCharacter")]
        private IMover _playerCharacter;

        bool _strafeRight;
        float _lastStrafeChangeTime;

        public void EnterState()
        {
            Debug.Log("Enter EnemyFollowState");

            _strafeRight = Random.Range(0, 1) == 0;
            _lastStrafeChangeTime = Time.realtimeSinceStartup;
        }

        public void ExitState()
        {
        }

        public void Update()
        {
            var distanceToPlyaer =
                (_playerCharacter.GetPosition() - _enemy.GetPosition()).magnitude;
            if (Time.realtimeSinceStartup - _lastStrafeChangeTime > 20)
            {
                _lastStrafeChangeTime = Time.realtimeSinceStartup;
                _strafeRight = !_strafeRight;
            }
        }

        public void FixedUpdate()
        {
            MoveTowardsPlayer();
            Strafe();
        }

        private void Strafe()
        {
            // Strafe to avoid getting hit too easily
            // if (_strafeRight)
            // {
            //     _enemyStateManager._enemy.AddForce(_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
            // }
            // else
            // {
            //     _enemyStateManager._enemy.AddForce(-_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
            // }
        }
        private void MoveTowardsPlayer()
        {
            var playerDir = (_playerCharacter.GetPosition() - _enemy.GetPosition()).normalized;

            _enemy.AddForce(playerDir * 15);
        }
    }
}