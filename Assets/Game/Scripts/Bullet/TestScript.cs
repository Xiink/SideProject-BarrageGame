using System;
using UnityEngine;

namespace Game.Scripts.Bullet
{
    public class TestScript : MonoBehaviour
    {
        public float speed = 10f;

        private void Update()
        {
            transform.Translate(speed*Time.deltaTime,0,0);
            Destroy(gameObject,3);
        }
    }
}