using UnityEngine;

namespace Game.Scripts.Battle.Misc
{
    public interface IMover
    {
        public Transform transform { get; }

        public float GetStatFinalValue(string statName);

        bool Moveable { get; }

        public Vector2 GetPosition();

        public void SetPosition(Vector2 newPos);
    }
}