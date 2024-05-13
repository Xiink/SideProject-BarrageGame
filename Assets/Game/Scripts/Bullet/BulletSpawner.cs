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
            var bullet = _bulletFactory.Create(1,BulletTypes.FromPlayer);

            bullet.transform.position = (Vector3)_player.GetPosition() - _player.transform.right;
            bullet.transform.rotation = _player.rigidbody.rotation;
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