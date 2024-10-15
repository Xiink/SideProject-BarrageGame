using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Bullet;
using Game.Scripts.Players.Events;
using Game.Scripts.Players.Main;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerShootHandler : ITickable
    {
        private readonly Bullet.Bullet.Factory _factory;

        [Inject] private IMover _player;
        [Inject] private PlayerInputState _playerInputState;

        [Inject]
        private List<PlayerShootObserver> test1;

        public PlayerShootHandler(Bullet.Bullet.Factory factory)
        {
             _factory = factory;
        }

        public void Tick()
        {
            if(_playerInputState._shootMouseDown == false) return;
            Fire();
        }

        public void Fire()
        {
            var type = BulletTypes.FromPlayer;
            var Position = (Vector3)_player.GetPosition() - _player.transform.right;
            var Rotation = _player.transform.rotation;
            var spawnData = new Bullet.Bullet.SpawnData(type, Position, Rotation);

            var bullet = _factory.Create(spawnData);

            // bullet.transform.position = (Vector3)_player.GetPosition() - _player.transform.right;
            // bullet.transform.rotation = _player.transform.rotation;

            foreach (var e in test1)
            {
                e.OnPlayerShoot();
            }
        }
    }
}