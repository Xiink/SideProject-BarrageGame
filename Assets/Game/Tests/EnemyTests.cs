using System.Collections.Generic;
using Assets.Game.Scripts.Enemy.Data;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Names;
using Game.Scripts.Players.Main;
using Game.Scripts.RPG;
using NSubstitute;
using NUnit.Framework;
using rStarUtility.Generic.TestExtensions;
using rStarUtility.Generic.TestFrameWork;
using UnityEngine;

namespace Game.Tests
{
    public class EnemyTests : TestFixture_DI_Log
    {
        [Test(Description = "初始化敵人，敵人數值正確")]
        public void Init_Enemy_Stats_Correct()
        {
            var moveHandler = Given_A_EnemyMoveHandler();
            var enemy = Resolve<Enemy>();


            enemy._data._domaindata = ScriptableObject.CreateInstance<DomainData>();
            enemy._data._domaindata.speed = 1;

            moveHandler.Move(new Vector2(1,1));
            enemy.Trans.ShouldTransformPositionBe(1, 1);
            enemy._data._domaindata.speed = 5;
            moveHandler.Move(new Vector2(1,1));
            enemy.Trans.ShouldTransformPositionBe(6, 6);
        }

        [Test(Description = "Add Force")]
        public void Add_Force_To_Enemy()
        {
            var EnemyMoveHander = Given_A_EnemyMoveHandler();
            var enemy = Resolve<Enemy>();

            enemy._data._domaindata = ScriptableObject.CreateInstance<DomainData>();
            enemy._data._domaindata.speed = 1;

            EnemyMoveHander.AddForce();
        }

        private EnemyMoveHandler Given_A_EnemyMoveHandler()
        {
            var enemy = NewEnemy();

            var timeProvider = Bind_Mock_And_Resolve<ITimeProvider>();
            timeProvider.GetDeltaTime().Returns(1);

            var moveHandler = Bind_And_Resolve<EnemyMoveHandler>();

            return moveHandler;
        }

        private Enemy NewEnemy()
        {
            if (HasBinding<IMoveable>() == false)
            {
                var moveable = Bind_Mock_And_Resolve<IMoveable>();
                moveable.GetState().Returns(true);
            }

            Bind_Instance(ScriptableObject.CreateInstance<DomainData>());
            Bind_Instance(ScriptableObject.CreateInstance<EnemyData>());
            // Bind_Instance(ScriptableObject.CreateInstance<VisualData>());
            // Container.Bind<EnemyData>().FromScriptableObjectResource("Assets/Game/Datas/NormalEnemyData.asset");
            Bind_InterfacesAndSelfTo_From_NewGameObject<Enemy>();

            Container.Bind<Rigidbody2D>().FromNewComponentOnNewGameObject().AsSingle();
            var rigibody2d = Container.Resolve<Rigidbody2D>();

            var enemy = Resolve<Enemy>();

            return enemy;
        }
    }
}