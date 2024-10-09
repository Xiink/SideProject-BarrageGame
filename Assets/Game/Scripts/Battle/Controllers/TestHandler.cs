using Game.Scripts.Enemy.Events;
using UnityEngine;

namespace Game.Scripts.Battle.Controllers
{
    public class TestHandler : NormalEnemyDieObserver
    {
        public void OnNormalEnemyDie()
        {
            Debug.Log("TestHandler OnNormalEnemyDie");
        }
    }
}