using UnityEngine;

namespace Game.Scripts.Battle.Misc
{
    public interface ITimeProvider
    {
        float GetDeltaTime();
    }

    public class TimeProvider : ITimeProvider
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }
    }
}