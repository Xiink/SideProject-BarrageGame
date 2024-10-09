using Game.Scripts.Enemy.Application;
using Game.Scripts.Enemy.Events;
using Zenject;

namespace Game.Scripts.Battle.Controllers
{
    public class BattleFlowController : ITickable, NormalEnemyDieObserver
    {
        [Inject]
        private EnemySpawner _enemySpawner;

        public void Tick()
        {

        }

        public void OnNormalEnemyDie()
        {
            _enemySpawner.CreateNewEnemy();
        }
    }
}