using Game.Scripts.Battle.Misc;
using Game.Scripts.Names;
using Game.Scripts.Players.Main;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerMoveHandler : ITickable
    {
        [Inject] public IMover _mover;
        [Inject] private PlayerInputState _playerInputState;
        [Inject] private ITimeProvider _timeProvider;
        [Inject] private ICameraProvider _cameraProvider;

        public void Tick()
        {
            if(_mover.Moveable == false) return;

            var moveSpeed = _mover.GetStatFinalValue(StatNames.MoveSpeed);
            var movement = _timeProvider.GetDeltaTime() * moveSpeed * _playerInputState.MoveDirection;
            var newPos = movement + _mover.GetPosition();
            _mover.SetPosition(newPos);

            var mousePos = _cameraProvider.GetMousePosition();
            var goalDir = mousePos - (Vector3)_mover.GetPosition();
            goalDir.z = 0;
            goalDir.Normalize();

            var d = Quaternion.LookRotation(goalDir) * Quaternion.AngleAxis(90, Vector3.up);
            _mover.rigidbody.rotation = d;
        }
    }
}