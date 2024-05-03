using Game.Scripts.Battle.States;
using rStarUtility.Util.Extensions.Unity;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Battle.Handlers
{
    public class GamePauseHandler : ITickable
    {
        [Inject] private InputState _inputState;

        [Inject] private GameState _gameState;

        [Inject(Id = "PausePanel")] private RectTransform panelPause;

        public void Tick()
        {
            if(_inputState._pauseKeyDown == false) return;
            var pause = _gameState._pause;

            panelPause.SetActive(!pause);

            _gameState.SetPauseState(!pause);
        }
    }
}