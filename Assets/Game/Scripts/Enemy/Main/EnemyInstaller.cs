using FluentAssertions;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enemy.Steps;
using Game.Scripts.RPG;
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
            Container.Bind<Enemy>().AsTransient().WithArguments(Container.Resolve<IEnemyDataFactory>().Create());

            Container.BindInterfacesTo<EnemyMoveHandler>().AsSingle();

            Container.BindInterfacesTo<EnemyEnterState>().AsSingle().WithArguments(GetComponent<IMover>());

            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle().WithArguments(GetComponent<IMover>());
            Container.BindInterfacesAndSelfTo<EnemyFollowState>().AsSingle();

            Container.Bind<EnemyFlowControl>().AsSingle();
            // Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle().WithArguments(GetComponent<IMover>());
            // Container.BindInterfacesAndSelfTo<EnemyFlowControl>().AsSingle().WithArguments(GetComponent<IMover>());
        }
    }
}