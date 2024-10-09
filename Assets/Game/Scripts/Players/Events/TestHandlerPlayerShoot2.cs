using UnityEngine;

namespace Game.Scripts.Players.Events
{
    public class TestHandlerPlayerShoot2 : PlayerShootObserver
    {
        public void OnPlayerShoot()
        {
            Debug.Log("TestHandlerPlayerShoot2 OnPlayerShoot");
        }
    }
}