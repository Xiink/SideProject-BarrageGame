using Game.Scripts.Battle.Handlers;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Bullet;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battle.Main
{
    public class BattleInstaller : MonoInstaller
    {
        public GameObject _bullet;
        public Camera mainCamera;

        public override void InstallBindings()
        {
            // Container.BindInstance(_bullet).IfNotBound();
            Container.BindFactory<BulletTypes, Bullet.Bullet, Bullet.Bullet.Factory>()
                .FromPoolableMemoryPool<BulletTypes, Bullet.Bullet, BulletScriptPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_bullet)
                    .UnderTransformGroup("Bullets"));

            Container.BindInstance<Camera>(mainCamera);
            Container.Bind<GameState>().AsSingle();
            Container.Bind<InputState>().AsSingle();

            Container.Bind<ITimeProvider>().To<TimeProvider>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.Bind<IMoveable>().To<Moveable>().AsSingle();
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GamePauseHandler>().AsSingle();

            Container.BindExecutionOrder<InputHandler>(-100000);
        }

        class BulletScriptPool : MonoPoolableMemoryPool<BulletTypes, IMemoryPool,Bullet.Bullet>
        {
        }
    }
}


