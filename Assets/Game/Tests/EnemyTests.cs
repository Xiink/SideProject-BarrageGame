using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Names;
using Game.Scripts.Players.Main;
using Game.Scripts.RPG;
using NSubstitute;
using NUnit.Framework;
using rStarUtility.Generic.TestFrameWork;

namespace Game.Tests
{
    public class EnemyTests : TestFixture_DI_Log
    {
        [Test(Description = "初始化敵人，敵人數值正確")]
        public void Init_Enemy_Stats_Correct()
        {
            var statDatas = new List<Stat.Data> { new Stat.Data(StatNames.MoveSpeed,999) };
            Bind_Instance(new Enemy.Data() {});

            // var moveHandler = Given_A_PlayerMoveHandler();
            // var playerCharacter = Resolve<PlayerCharacter>();
        }

        private EnemyMoveHandler Given_A_EnemyMoveHandler()
        {
            var enemy = NewEnemy();


            var moveHandler = Bind_Mock_And_Resolve<EnemyMoveHandler>();
            return moveHandler;
        }

        private Enemy NewEnemy()
        {
            if (HasBinding<IMoveable>() == false)
            {
                var moveable = Bind_Mock_And_Resolve<IMoveable>();
                moveable.GetState().Returns(true);
            }

            Bind_Instance(new Enemy.Data());
            Bind_InterfacesAndSelfTo_From_NewGameObject<Enemy>();
            var enemy = Resolve<Enemy>();

            return enemy;
        }
    }
}