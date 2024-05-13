using System.Runtime.InteropServices;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerInputHandler : ITickable
    {
        [Inject] private PlayerInputState _playerInputState;

        public void Tick()
        {
            _playerInputState.SetHor(Input.GetAxisRaw("Horizontal"));
            _playerInputState.SetVer(Input.GetAxisRaw("Vertical"));

            _playerInputState.SetShootMouseDown(Input.GetMouseButtonDown(0));
        }
    }
}