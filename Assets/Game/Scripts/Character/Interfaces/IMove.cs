using UnityEngine;

namespace Game.Scripts.Character.Interfaces
{
    public interface IMove
    {
        public Transform transform { get; set; }

        bool Moveable { get; }

        public Vector2 GetPosition();

        public void SetPosition(Vector2 newPos);

        public void AddForce(Vector2 force);
    }
}