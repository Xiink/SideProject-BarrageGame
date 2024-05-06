using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    public class Bullet : MonoBehaviour,IPoolable<float,IMemoryPool>
    {
        public float speed = 10f;
        public
        IMemoryPool _pool;

        private void Update()
        {
            transform.Translate(speed*Time.deltaTime,0,0);
            Destroy(gameObject,3);
        }

        public class Factory : PlaceholderFactory<float,Bullet>
        {

        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(float speed, IMemoryPool p3)
        {
            _pool = p3;
            speed = speed;
        }
    }
}