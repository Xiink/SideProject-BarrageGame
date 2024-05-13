using System;
using UnityEngine;

namespace Game.Scripts.Players.Main
{
    public class PlayerInputState
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public Vector2 MoveDirection => new Vector2(Horizontal, Vertical);
        public bool _shootMouseDown { get; private set; }

        public void SetMoveDirection(float hor, float ver)
        {
            SetHor(hor);
            SetVer(ver);
        }

        public void SetVer(float ver)
        {
            Vertical = ver;
        }

        public void SetHor(float hor)
        {
            Horizontal = hor;
        }

        public void SetShootMouseDown(bool state)
        {
            _shootMouseDown = state;
        }
    }
}