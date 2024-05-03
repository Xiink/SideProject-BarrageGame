using Game.Scripts.Battle.States;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battle.Handlers
{
    public class InputHandler : ITickable
    {
        [Inject] private InputState _inputState;

        public void Tick()
        {
            _inputState.SetPauseKeyDown(Input.GetKeyDown(KeyCode.Escape));
        }
    }
}