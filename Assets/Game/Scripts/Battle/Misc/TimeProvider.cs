using UnityEngine;

namespace Game.Scripts.Battle.Misc
{
    public interface ITimeProvider
    {
        float GetDeltaTime();

        float GetRealtimeSinceStartup();
    }

    public class TimeProvider : ITimeProvider
    {
        public float GetDeltaTime()
        {
            return Time.deltaTime;
        }

        public float GetRealtimeSinceStartup()
        {
            return Time.realtimeSinceStartup;
        }
    }
}