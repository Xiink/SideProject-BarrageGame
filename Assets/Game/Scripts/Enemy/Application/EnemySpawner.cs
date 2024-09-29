using UnityEngine;
using Zenject;

namespace Game.Scripts.Enemy.Application
{
    public class EnemySpawner
    {
        [Inject] private Enemy.Factory _factory;

        public void CreateNewEnemy()
        {
            if (_factory == null)
            {
                Debug.LogError("Enemy.Factory not injected!");
            }
            else
            {
                var enemy = _factory.Create();
                Debug.Log("Enemy created successfully.");
            }
        }
    }
}