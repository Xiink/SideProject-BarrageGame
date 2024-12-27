using Game.Scripts.Bullet.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    /// <summary>
    /// 繼承一個IFactory介面，並且實作這個介面
    /// 這個CustomBulletFactory是可以被替換的
    /// </summary>
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