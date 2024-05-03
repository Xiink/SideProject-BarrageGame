using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Battle.States;
using Game.Scripts.Names;
using Game.Scripts.Players.Handlers;
using Game.Scripts.Players.Main;
using Game.Scripts.RPG;
using NSubstitute;
using NUnit.Framework;
using rStarUtility.Generic.TestExtensions;
using rStarUtility.Generic.TestFrameWork;

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

            moveHandler.Tick();
            playerCharacter.Trans.ShouldTransformPositionBe(1, 1);
            moveHandler.Tick();
            playerCharacter.Trans.ShouldTransformPositionBe(2, 2);

        }

        [Test(Description = "遊戲暫停，玩家無法移動角色")]
        public void BattlePause_Cannot_MovePlayerCharacter()
        {
            var gameState = Bind_And_Resolve<GameState>();
            Bind_InterfacesTo<Moveable>();

            var moverHandler = Given_A_PlayerMoveHandler();

            var playerCharacter = Resolve<PlayerCharacter>();
            gameState.SetPauseState(true);
            moverHandler.Tick();
            playerCharacter.Trans.ShouldTransformPositionBe(0,0);
            gameState.SetPauseState(false);
            moverHandler.Tick();
            playerCharacter.Trans.ShouldTransformPositionBe(1,1);

        }

        [Test(Description = "設定數值時，會限制數值最大最小值")]
        // [Ignore("還沒做完，正確為計算值的設置")]
        public void Set_PlayerCharacter_Stats_WouldBe_Clamp()
        {
            // var statName = "123";
            // var statDatas = new List<Stat.Data> { new Stat.Data(statName, 0, 2, 99) };
            // Bind_Instance(new PlayerCharacter.Data() { statDatas = statDatas });
            // var character = NewPlayerCharacter();
            // character.GetStatFinalValue(statName).ShouldBe(2);
            // character.SetStatAmount(statName, -5);
            // character.GetStatFinalValue(statName).ShouldBe(2);
        }

        private PlayerMoveHandler Given_A_PlayerMoveHandler()
        {
            var playerCharacter = NewPlayerCharacter();
            playerCharacter.SetStatAmount(StatNames.MoveSpeed,1);
            var inputState = Bind_And_Resolve<PlayerInputState>();
            var timeProvider = Bind_Mock_And_Resolve<ITimeProvider>();
            inputState.SetMoveDirection(1, 1);
            timeProvider.GetDeltaTime().Returns(1);

            var moveHandler = Bind_And_Resolve<PlayerMoveHandler>();
            return moveHandler;
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
            var playerCharacter = Resolve<PlayerCharacter>();
            return playerCharacter;
        }

    }
}