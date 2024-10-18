using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Bullet;
using Game.Scripts.Bullet.Interfaces;
using Game.Scripts.Players.Events;
using Game.Scripts.Players.Main;
using Zenject;
using UnityEngine;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerShootHandler : ITickable
    {
        private readonly IBulletFactory _bulletFactory;

        [Inject] private IMover _player;
        [Inject] private PlayerInputState _playerInputState;

        [Inject]
        private List<PlayerShootObserver> test1;

        // [Inject]
        // private TestFactory.TestFactory.ObjFactory _customFactory;

        // [Inject]
        // private IBulletFactory _bulletFactory;
        // private Bullet.BulletFactory _bulletFactory;

        public PlayerShootHandler(IBulletFactory factory)
        {
            _bulletFactory = factory;
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

            // var bullet = _factory.Create(spawnData);
            CreateBullet(spawnData);

            // var obj = _customFactory.Create(new TestFactory.TestFactory.SpawnData(Position, Rotation));
            // obj.OnCreated();

            foreach (var e in test1)
            {
                e.OnPlayerShoot();
            }
        }

        public void CreateBullet(Bullet.Bullet.SpawnData data)
        {
            _bulletFactory.Create(data);
        }
    }
}