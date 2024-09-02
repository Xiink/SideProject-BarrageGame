using Game.Scripts.Battle.Misc;
using Game.Scripts.Names;
using Game.Scripts.Players.Main;
using rStarUtility.Util.Helper;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Handlers
{
    public class PlayerMoveHandler : ITickable
    {
        // [Inject(Id = "PlayerCharacter")]
        [Inject]
        public IMover _mover;
        [Inject] private PlayerInputState _playerInputState;
        [Inject] private ITimeProvider _timeProvider;
        [Inject] private ICameraProvider _cameraProvider;

        public void Tick()
        {
            if (_playerInputState._dashKeyDown == true)
            {
                if(Math.Abs(_playerInputState.Horizontal) > Math.Abs(_playerInputState.Vertical))
                    HorDash(5);
                else
                {
                    VerDash(5);
                }
            }
            else
            {
                Move();
            }

            GetMousePosition();
        }

        public void GetMousePosition()
        {
            if(_mover.Moveable == false) return;

            var mousePos = _cameraProvider.GetMousePosition();
            var goalDir = mousePos - (Vector3)_mover.GetPosition();
            goalDir.z = 0;
            goalDir.Normalize();

            var d = Quaternion.LookRotation(goalDir) * Quaternion.AngleAxis(90, Vector3.up);
            _mover.rigidbody.rotation = d;
        }

        public void Move()
        {
            if(_mover.Moveable == false) return;

            var moveSpeed = _mover.GetStatFinalValue(StatNames.MoveSpeed);
            var movement = _timeProvider.GetDeltaTime() * moveSpeed * _playerInputState.MoveDirection;
            var newPos = movement + _mover.GetPosition();
            _mover.SetPosition(newPos);
        }

        public void HorDash(int frame)
        {
            if(_mover.Moveable == false) return;

            var moveSpeed = _mover.GetStatFinalValue(StatNames.MoveSpeed);

            for (int i = 0; i < frame; i++)
            {
                var movement = _timeProvider.GetDeltaTime() * moveSpeed * new Vector2(_playerInputState.Horizontal,0);
                var newPos = movement + _mover.GetPosition();
                _mover.SetPosition(newPos);
            }
        }

        public void VerDash(int frame)
        {
            if(_mover.Moveable == false) return;

            var moveSpeed = _mover.GetStatFinalValue(StatNames.MoveSpeed);

            for (int i = 0; i < frame; i++)
            {
                var movement = _timeProvider.GetDeltaTime() * moveSpeed * new Vector2(0,_playerInputState.Vertical);
                var newPos = movement + _mover.GetPosition();
                _mover.SetPosition(newPos);
            }
        }
    }
}