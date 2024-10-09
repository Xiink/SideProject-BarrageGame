using Game.Scripts.Enemy.Events;
using UnityEngine;

namespace Game.Scripts.Battle.Controllers
{
    public class GetEnemyDieHandler : NormalEnemyDieObserver
    {
        public void OnNormalEnemyDie()
        {
            Debug.Log("GetEnemyDieHandler OnNormalEnemyDie");
        }
    }
}