using System.Collections.Generic;
using Assets.Game.Scripts.Enemy.Data;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Enemy.UI;
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

            enemy._data._domaindata.Datas = new List<Stat.Data>();
            enemy._data._domaindata.Datas.Add(new Stat.Data(StatNames.MoveSpeed, 1));
            enemy.InitStats();

            moveHandler.Move(new Vector2(1,1));
            enemy.Trans.ShouldTransformPositionBe(1, 1);
            enemy.SetStatAmount(StatNames.MoveSpeed, 5);
            moveHandler.Move(new Vector2(1,1));
            enemy.Trans.ShouldTransformPositionBe(6, 6);
        }

        [Test(Description = "扣血，正確計算剩餘生命值")]
        public void Deduct_Enemy_Hp_Correct()
        {
            var enemy = NewEnemy();

            enemy._data._domaindata.Datas = new List<Stat.Data>();
            enemy._data._domaindata.Datas.Add(new Stat.Data(StatNames.Hp, 10));
            enemy.InitStats();

            enemy.CalculateHp(1, out var percent);

            Assert.AreEqual(9, enemy.GetStatFinalValue(StatNames.Hp));
        }

        [Test(Description = "Add Force")]
        public void Add_Force_To_Enemy()
        {
            var EnemyMoveHander = Given_A_EnemyMoveHandler();
            var enemy = Resolve<Enemy>();
            var initialPosition = enemy.rigidbody2D.position;

            EnemyMoveHander.AddForce();

            // 調成SimulationMode2D.Script，才能模擬物理效果
            Physics2D.simulationMode = SimulationMode2D.Script;
            Physics2D.Simulate(Time.fixedDeltaTime);

            Assert.AreNotEqual(initialPosition, enemy.rigidbody2D.position);
            Assert.Greater(enemy.rigidbody2D.position.x, initialPosition.x);

            // 要記得改回來，不然會影響其他測試跟Runtime
            Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
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

            // Bind_Instance(ScriptableObject.CreateInstance<DomainData>());
            Bind_Instance(ScriptableObject.CreateInstance<EnemyData>());
            var enemyData = Container.Resolve<EnemyData>();
            enemyData._domaindata = ScriptableObject.CreateInstance<DomainData>();
            enemyData._visualData = ScriptableObject.CreateInstance<VisualData>();
            // Bind_Instance(ScriptableObject.CreateInstance<VisualData>());
            // Container.Bind<EnemyData>().FromScriptableObjectResource("Assets/Game/Datas/NormalEnemyData.asset");

            Bind_InterfacesAndSelfTo_From_NewGameObject<Enemy>();

            Container.Bind<EnemyHpBar>().WithId("NormalEnemy").FromNewComponentOnNewGameObject().AsSingle();
            var enemyHpBar = Container.ResolveId<EnemyHpBar>("NormalEnemy");

            Container.Bind<SpriteRenderer>().WithId("NormalEnemy").FromNewComponentOnNewGameObject().AsSingle();
            var spriteRenderer = Container.ResolveId<SpriteRenderer>("NormalEnemy");

            Container.Bind<Rigidbody2D>().WithId("NormalEnemy").FromNewComponentOnNewGameObject().AsSingle();
            var rigibody2d = Container.ResolveId<Rigidbody2D>("NormalEnemy");

            var enemy = Resolve<Enemy>();

            return enemy;
        }
    }
}