using Game.Scripts.Enemy.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Main
{
    public interface IEnemyDataFactory
    {
        EnemyData Create();
    }

    public class EnemyDataFactory : IEnemyDataFactory
    {
        private readonly EnemyData  _enemyDataFactory;
        public EnemyDataFactory(EnemyData  enemyFactory)
        {
            _enemyDataFactory = enemyFactory;
        }

        public EnemyData Create()
        {
            // 複製一個EnemyData
            return ScriptableObject.Instantiate(_enemyDataFactory);
        }
    }
}