using UnityEngine;

namespace Game.Scripts.Players.Events
{
    public class TestHandlerPlayerShoot : PlayerShootObserver
    {
        public void OnPlayerShoot()
        {
            Debug.Log("TestHandlerPlayerShoot OnPlayerShoot");
        }
    }
}