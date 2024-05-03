using Game.Scripts.Battle.States;
using NSubstitute.Core;
using Zenject;

namespace Game.Scripts.Battle.Misc
{
    public class Moveable : IMoveable
    {
        [Inject] private GameState _gameState;

        public bool GetState()
        {
            var result = _gameState._pause == false;
            return result;
        }
    }
}