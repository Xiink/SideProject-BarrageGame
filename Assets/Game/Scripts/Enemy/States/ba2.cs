// using Game.Scripts.Enemy.Main;
// using Game.Scripts.Players.Main;
// using UnityEngine;
// using Zenject;
//
// namespace Game.Scripts.Enemy.States
// {
//     public class EnemyFollowState : IEnemyState
//     {
//         // [Inject]
//         readonly EnemyStateManager _EnemyStateManager;
//
//         bool _strafeRight;
//         float _lastStrafeChangeTime;
//
//         public EnemyFollowState(EnemyStateManager enemyStateManager)
//         {
//             _EnemyStateManager = enemyStateManager;
//         }
//
//         public void EnterState()
//         {
//             _strafeRight = Random.Range(0, 1) == 0;
//             _lastStrafeChangeTime = Time.realtimeSinceStartup;
//         }
//
//         public void ExitState()
//         {
//         }
//
//         public void Update()
//         {
//             // var distanceToPlyaer = (_EnemyStateManager._playerCharacter.GetPosition() - _EnemyStateManager._enemy.GetPosition()).magnitude;
//             // if (Time.realtimeSinceStartup - _lastStrafeChangeTime > 20)
//             // {
//             //     _lastStrafeChangeTime = Time.realtimeSinceStartup;
//             //     _strafeRight = !_strafeRight;
//             // }
//
//         }
//
//         public void FixedUpdate()
//         {
//             MoveTowardsPlayer();
//             Strafe();
//         }
//
//         private void Strafe()
//         {
//             // Strafe to avoid getting hit too easily
//             // if (_strafeRight)
//             // {
//             //     _EnemyStateManager._enemy.AddForce(_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
//             // }
//             // else
//             // {
//             //     _EnemyStateManager._enemy.AddForce(-_view.RightDir * _settings.StrafeMultiplier * _tunables.Speed);
//             // }
//         }
//
//         private void MoveTowardsPlayer()
//         {
//             // var playerDir = (_EnemyStateManager._playerCharacter.GetPosition() - _EnemyStateManager._enemy.GetPosition()).normalized;
//             //
//             // _EnemyStateManager._enemy.AddForce(playerDir * 15);
//         }
//     }
// }