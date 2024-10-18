using System.Collections.Generic;
using System.ComponentModel;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Bullet;
using Game.Scripts.Bullet.Interfaces;
using Game.Scripts.Names;
using Game.Scripts.Players.Events;
using Game.Scripts.Players.Handlers;
using Game.Scripts.Players.Main;
using Game.Scripts.RPG;
using NSubstitute;
using NUnit.Framework;
using rStarUtility.Generic.TestExtensions;
using rStarUtility.Generic.TestFrameWork;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Tests
{
    public class PlayerCharacterTests : TestFixture_DI_Log
    {
        [Test(Description = "初始化角色，角色數值正確")]
        public void Init_PlayerCharacter_Stats_Correct()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed,999) };
            Bind_Instance(new PlayerCharacter.Data() {statDatas = statDatas});

            var moveHandler = Given_A_PlayerMoveHandler();
            var playerCharacter = Resolve<PlayerCharacter>();
            // Bind_Instance(new PlayerCharacter.Data());
            // Bind_InterfacesAndSelfTo_From_NewGameObject<PlayerCharacter>();
            // var playerCharacter = Resolve<PlayerCharacter>();


            playerCharacter.Stats.CountShouldBe(1);
        }

        [Test(Description = "透過玩家輸入，移動玩家角色")]
        public void MovePlayerCharacter_By_PlayerInput()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed, 999) };
            Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });

            var moveHandler = Given_A_PlayerMoveHandler();
            var playerCharacter = Resolve<PlayerCharacter>();

            moveHandler.Move();
            playerCharacter.Trans.ShouldTransformPositionBe(1, 1);
            moveHandler.Move();
            playerCharacter.Trans.ShouldTransformPositionBe(2, 2);

        }

        [Test(Description = "玩家使用Dash")]
        public void DashPlayerCharacter_By_Input()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed, 999) };
            Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });

            var moveHandler = Given_A_PlayerMoveHandler();
            var playerCharacter = Resolve<PlayerCharacter>();

            moveHandler.HorDash(5);
            playerCharacter.Trans.ShouldTransformPositionBe(5, 0);
            moveHandler.VerDash(5);
            playerCharacter.Trans.ShouldTransformPositionBe(5, 5);
        }


        [Test(Description = "遊戲暫停，玩家無法移動角色")]
        public void BattlePause_Cannot_MovePlayerCharacter()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed, 999) };
            Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });

            var gameState = Bind_And_Resolve<GameState>();
            Bind_InterfacesTo<Moveable>();

            var moverHandler = Given_A_PlayerMoveHandler();

            var playerCharacter = Resolve<PlayerCharacter>();

            gameState.SetPauseState(true);
            moverHandler.Move();
            playerCharacter.Trans.ShouldTransformPositionBe(0,0);
            gameState.SetPauseState(false);
            moverHandler.Move();
            playerCharacter.Trans.ShouldTransformPositionBe(1,1);

        }

        [Test(Description = "玩家角色射擊")]
        public void PlayerCharacter_Shoot()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed, 999) };
            Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });

            // Container.BindFactoryCustomInterface<Bullet.SpawnData,IBullet,BulletFactory,IBulletFactory>()
            //     .FromFactory<CustomBulletFactory>();
            // GameObject _bullet = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Game/Datas/Bullet.prefab");
            // Container.BindFactory<Bullet, Bullet.Factory>().FromComponentInNewPrefab(_bullet);
            var IFactory = Substitute.For<IBulletFactory>();
            Bind_Instance(IFactory);

            var shootHandler = Given_A_PlayerShootHandler();
            var playerCharacter = Resolve<PlayerCharacter>();

            // IFactory.Create(new Bullet.SpawnData(BulletTypes.Other,Vector3.zero,Quaternion.identity)).Returns(Substitute.For<IBullet>());
            //
            // shootHandler.Tick();
            var spawnData = new Bullet.SpawnData(BulletTypes.Other, Vector3.zero, Quaternion.identity);
            shootHandler.CreateBullet(spawnData);

            IFactory.Received(1).Create(spawnData);
            // Assert.AreEqual(1,Object.FindObjectsOfType<Bullet>().Length);
        }

        [Test(Description = "設定數值時，會限制數值最大最小值")]
        // [Ignore("還沒做完，正確為計算值的設置")]
        public void set_PlayerCharacter_Stats_WouldBe_Clamp()
        {
            var statName = StatNames.MoveSpeed;
            var statDatas = new List<Stat.Data> { new Stat.Data(statName, 9999) };
            Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });
            var character = NewPlayerCharacter();
            character.GetStatFinalValue(statName).ShouldBe(30);
            character.SetStatAmount(statName, -5);
            character.GetStatFinalValue(statName).ShouldBe(1);
        }

        private PlayerMoveHandler Given_A_PlayerMoveHandler()
        {
            var playerCharacter = NewPlayerCharacter();
            playerCharacter.SetStatAmount(StatNames.MoveSpeed,1);

            var inputState = Bind_And_Resolve<PlayerInputState>();
            var timeProvider = Bind_Mock_And_Resolve<ITimeProvider>();
            var cameraProvider = Bind_Mock_And_Resolve<ICameraProvider>();

            inputState.SetMoveDirection(1, 1);
            timeProvider.GetDeltaTime().Returns(1);

            var moveHandler = Bind_And_Resolve<PlayerMoveHandler>();

            return moveHandler;
        }

        private PlayerShootHandler Given_A_PlayerShootHandler()
        {
            var playerCharacter = NewPlayerCharacter();
            playerCharacter.SetStatAmount(StatNames.MoveSpeed,1);

            var inputState = Bind_And_Resolve<PlayerInputState>();
            var timeProvider = Bind_Mock_And_Resolve<ITimeProvider>();
            var cameraProvider = Bind_Mock_And_Resolve<ICameraProvider>();

            var test1 = Container.BindInterfacesTo<TestHandlerPlayerShoot>().AsSingle();
            var test2 = Container.BindInterfacesTo<TestHandlerPlayerShoot2>().AsSingle();

            inputState.SetMoveDirection(1, 1);
            timeProvider.GetDeltaTime().Returns(1);

            var shootHandler = Bind_And_Resolve<PlayerShootHandler>();

            return shootHandler;
        }

        private PlayerCharacter NewPlayerCharacter()
        {
            if (HasBinding<IMoveable>() == false)
            {
                var moveable = Bind_Mock_And_Resolve<IMoveable>();
                moveable.GetState().Returns(true);
            }

            Bind_Instance(new PlayerCharacter.Data());

            Bind_InterfacesAndSelfTo_From_NewGameObject<PlayerCharacter>();
            Container.Bind<Rigidbody2D>().WithId("PlayerCharacter").FromNewComponentOnNewGameObject().AsSingle();
            var rigibody = Container.ResolveId<Rigidbody2D>("PlayerCharacter");

            var playerCharacter = Resolve<PlayerCharacter>();
            return playerCharacter;
        }
    }
}