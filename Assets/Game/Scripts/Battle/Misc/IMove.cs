using UnityEngine;

namespace Game.Scripts.Battle.Misc
{
    public interface IMover
    {
        public Transform transform { get; }

        public Rigidbody rigidbody { get; }

        public float GetStatFinalValue(string statName);

        bool Moveable { get; }

        public Vector2 GetPosition();

        public void AddForce(Vector3 force);

        public void SetPosition(Vector2 newPos);
    }
}