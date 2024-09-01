using Game.Scripts.Battle.Misc;
using Game.Scripts.Bullet;
using Game.Scripts.Enemy.Main;
using Game.Scripts.Players.Handlers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Main
{
    public class PlayerCharacterInstaller : MonoInstaller
    {

        public GameObject _player;

        public PlayerCharacterInstaller()
        {
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerInputState>().AsSingle();

            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().WithArguments(GetComponent<IMover>());
            Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle().WithArguments(GetComponent<IMover>());
            // Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().WithArguments(GetComponent<IMover>());
            // Container.BindInterfacesAndSelfTo<PlayerShootHandler>().AsSingle().WithArguments(GetComponent<IMover>());

            Container.BindExecutionOrder<PlayerInputHandler>(-10000);
        }
    }
}