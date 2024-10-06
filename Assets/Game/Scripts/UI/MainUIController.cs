using Game.Scripts.Enemy.Application;
using rStarUtility.Util.Extensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    public class MainUIController : IInitializable
    {
        [Inject(Id = "CreateEnemy")]
        private Button _createEnemyBtn;

        [Inject]
        private EnemySpawner _enemySpawner;

        public void Initialize()
        {
            _createEnemyBtn.BindClick(CreateEnemy);
        }

        private void CreateEnemy()
        {
            Debug.Log("Test");
            _enemySpawner.CreateNewEnemy();
        }
    }
}