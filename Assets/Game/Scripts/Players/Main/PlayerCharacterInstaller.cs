using Game.Scripts.Battle.Misc;
using Game.Scripts.Bullet;
using Game.Scripts.Enemy.Main;
using Game.Scripts.Players.Handlers;
using Zenject;

namespace Game.Scripts.Players.Main
{
    public class PlayerCharacterInstaller : MonoInstaller
    {


        public PlayerCharacterInstaller()
        {
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputState>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().WithArguments(GetComponent<IMover>());
            Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle().WithArguments(GetComponent<IMover>());

            Container.BindExecutionOrder<PlayerInputHandler>(-10000);
        }
    }
}