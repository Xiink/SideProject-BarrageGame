using Game.Scripts.Battle.Controllers;
using Game.Scripts.Battle.Handlers;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Bullet;
using Game.Scripts.Enemy.Application;
using Game.Scripts.Enemy.Events;
using Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battle.Main
{
    public class BattleInstaller : MonoInstaller
    {
        public GameObject _bullet;
        public GameObject enemyPrefab;
        public Camera mainCamera;

        public override void InstallBindings()
        {
            BindSpawner();
            BindState();
            BindHandler();
            BindController();

            Container.BindInstance<Camera>(mainCamera);
            Container.Bind<ITimeProvider>().To<TimeProvider>().AsSingle();
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            Container.Bind<IMoveable>().To<Moveable>().AsSingle();
            Container.BindInterfacesTo<MainUIController>().AsSingle();
        }

        public void BindController()
        {
            Container.BindInterfacesAndSelfTo<BattleFlowController>().AsSingle();
        }

        public void BindSpawner()
        {
            Container.BindFactory<BulletTypes, Bullet.Bullet, Bullet.Bullet.Factory>()
                .FromPoolableMemoryPool<BulletTypes, Bullet.Bullet, BulletScriptPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(_bullet)
                    .UnderTransformGroup("Bullets"));

            Container.BindFactory<Enemy.Enemy, Enemy.Enemy.Factory>()
                .FromPoolableMemoryPool<Enemy.Enemy, EnemyScriptPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(enemyPrefab)
                    .UnderTransformGroup("Enemies").AsTransient());

            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        }

        public void BindState()
        {
            Container.Bind<GameState>().AsSingle();
            Container.Bind<InputState>().AsSingle();
        }

        public void BindHandler()
        {
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GamePauseHandler>().AsSingle();

            Container.BindExecutionOrder<InputHandler>(-100000);
        }

        class BulletScriptPool : MonoPoolableMemoryPool<BulletTypes, IMemoryPool,Bullet.Bullet>
        {
        }

        class EnemyScriptPool : MonoPoolableMemoryPool<IMemoryPool,Enemy.Enemy>
        {
        }
    }
}


