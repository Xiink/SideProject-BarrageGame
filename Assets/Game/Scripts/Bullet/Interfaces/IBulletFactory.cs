using Zenject;

namespace Game.Scripts.Bullet.Interfaces
{
    public interface IBulletFactory : IFactory<Bullet.SpawnData,IBullet>
    {

    }
}