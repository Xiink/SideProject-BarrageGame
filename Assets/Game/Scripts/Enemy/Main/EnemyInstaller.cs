using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.States;
using Game.Scripts.Enemy.Steps;
using Game.Scripts.RPG;
using Zenject;

namespace Game.Scripts.Enemy.Main
{
    public class EnemyInstaller : MonoInstaller
    {
        public EnemyInstaller()
        {

        }

        public override void InstallBindings()
        {
            // Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();
            Container.BindInterfacesTo<EnemyEnterState>().AsSingle().WithArguments(GetComponent<IMover>());
            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFlowControl>().AsSingle().WithArguments(GetComponent<IMover>());

        }
    }
}