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
        public GameObject test;

        public override void InstallBindings()
        {
            Container.BindInstance(test).IfNotBound();
            Container.BindFactory<float, Bullet.Bullet, Bullet.Bullet.Factory>()
                .FromPoolableMemoryPool<float, Bullet.Bullet, TestScriptPool>(poolBinder => poolBinder
                    .WithInitialSize(20)
                    .FromComponentInNewPrefab(test));

            Container.Bind<GameState>().AsSingle();
            Container.Bind<InputState>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimeProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<Moveable>().AsSingle();
            Container.BindInterfacesTo<InputHandler>().AsSingle();
            Container.BindInterfacesTo<GamePauseHandler>().AsSingle();

            Container.BindExecutionOrder<InputHandler>(-100000);
        }

        class TestScriptPool : MonoPoolableMemoryPool<float, IMemoryPool,Bullet.Bullet>
        {
        }
    }
}


