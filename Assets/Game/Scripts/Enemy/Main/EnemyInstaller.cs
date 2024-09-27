using FluentAssertions;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enemy.Steps;
using Game.Scripts.RPG;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Main
{
    public class EnemyInstaller : MonoInstaller
    {
        public EnemyData enemyData;

        public EnemyInstaller()
        {

        }

        public override void InstallBindings()
        {
            // Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();
            // Container.BindInterfacesAndSelfTo<EnemyFlowControl>().AsSingle();
            Container.Bind<IEnemyDataFactory>().To<EnemyDataFactory>().AsTransient().WithArguments(enemyData);
            // 使用工厂创建的 EnemyData 绑定 Enemy
            Container.Bind<Enemy>().AsTransient().OnInstantiated<Enemy>((ctx, enemy) =>
            {
                var factory = ctx.Container.Resolve<IEnemyDataFactory>();
                enemy.Construct(factory.Create());
            });

            Container.BindInterfacesTo<EnemyMoveHandler>().AsSingle();

            Container.BindInterfacesTo<EnemyEnterState>().AsSingle().WithArguments(GetComponent<IMover>());

            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle().WithArguments(GetComponent<IMover>());
            Container.BindInterfacesAndSelfTo<EnemyFollowState>().AsSingle();

            Container.Bind<EnemyFlowControl>().AsSingle();
        }

    }
}