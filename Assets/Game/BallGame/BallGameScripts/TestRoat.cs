using UnityEngine;
using Sirenix.OdinInspector;

namespace Game.BallGame.Scripts
{
    public class TestRoat : MonoBehaviour
    {
        [PropertyRange(0,1)]
        public float speed = 0.05f;

        void Update()
        {
            // 不斷地只旋轉Z軸
            transform.Rotate(new Vector3(0,0,-1), speed);
        }
    }
}