using Game.Scripts.Bullet.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    public class CustomBulletFactory : IFactory<Bullet.SpawnData,IBullet>
    {
        private readonly Bullet.Factory _factory;

        public CustomBulletFactory(Bullet.Factory factory)
        {
            _factory = factory;
        }

        public IBullet Create(Bullet.SpawnData param)
        {
            var newBullet = _factory.Create();
            newBullet.OnSpawned(param);
            return newBullet;
        }

    }
}