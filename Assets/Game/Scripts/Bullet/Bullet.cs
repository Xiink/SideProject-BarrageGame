using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    public enum BulletTypes
    {
        FromEnemy,
        FromPlayer,
        Other
    }

    public class Bullet : MonoBehaviour,IPoolable<float,BulletTypes,IMemoryPool>
    {
        public float speed = 50f;
        public IMemoryPool _pool;
        public BulletTypes _type;

        private void Update()
        {
            transform.Translate(speed*Time.deltaTime*-1,0,0);
            Destroy(gameObject,3);
        }

        public class Factory : PlaceholderFactory<float,BulletTypes,Bullet>
        {

        }

        public void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.Die();
                _pool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(float speed,BulletTypes type, IMemoryPool p3)
        {
            speed = speed;
            _type = type;
            _pool = p3;
        }
    }
}