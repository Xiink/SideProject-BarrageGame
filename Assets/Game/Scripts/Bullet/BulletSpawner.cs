using System;
using Game.Scripts.Battle.Misc;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    public class BulletSpawner : ITickable
    {
        readonly Bullet.Factory _bulletFactory;
        [Inject] private IMover _player;

        public BulletSpawner(Bullet.Factory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void Fire()
        {
            var bullet = _bulletFactory.Create(1);

            bullet.transform.position = _player.GetPosition();
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }
}