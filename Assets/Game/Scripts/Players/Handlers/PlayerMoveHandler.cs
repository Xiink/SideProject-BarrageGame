using Game.Scripts.Battle.Misc;
using Game.Scripts.Names;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerMoveHandler : ITickable
    {
        [Inject] private IMover _mover;
        [Inject] private PlayerInputState _playerInputState;
        [Inject] private ITimeProvider _timeProvider;

        public void Tick()
        {
            var moveSpeed = _mover.GetStatFinalValue(StatNames.MoveSpeed);
            var movement = _timeProvider.GetDeltaTime() * moveSpeed * _playerInputState.MoveDirection;
            var newPos = movement + _mover.GetPosition();
            _mover.SetPosition(newPos);
        }
    }
}