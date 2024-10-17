using Game.Scripts.Bullet.Interfaces;
using Zenject;

namespace Game.Scripts.Bullet
{
    public class BulletFactory :  PlaceholderFactory<Bullet.SpawnData,IBullet>,IBulletFactory
    {

    }
}