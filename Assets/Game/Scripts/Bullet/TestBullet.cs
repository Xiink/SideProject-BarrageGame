using Game.Scripts.Bullet.Interfaces;
using Game.Scripts.Names;
using Game.Scripts.RPG;
using rStarUtility.Generic.Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Bullet
{
    public class TestBullet : MonoBehaviour, IBullet
    {
        #region Public Variables

        public IMemoryPool _pool;
        public BulletTypes _type;

        #endregion

        #region Private Variables

        float _startTime;
        float _lifeTime = 3f;

        [Inject] private Bullet.Data _data;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Unity events

        private void Update()
        {
            transform.Translate(FindStat(StatNames.MoveSpeed).stat.Amount * Time.deltaTime * -1, 0, 0);

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                // _pool.Despawn(this);
                Destroy(this);
            }
        }

        #endregion

        #region Public Methods

        public float GetStatFinalValue(string statName)
        {
            var finalValue = 0f;
            var (contains, stat) = FindStat(statName);
            if (contains)
            {
                finalValue = stat.Amount;
            }

            return finalValue;
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(Bullet.SpawnData data, IMemoryPool p3)
        {
            //speed = speed;
            _type = data.Type;
            _pool = p3;

            _startTime = Time.realtimeSinceStartup;

            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter");
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.TakeDamage();
                _pool.Despawn(this);
            }
        }

        #endregion

        #region Private Methods

        private (bool containsStat, Stat stat) FindStat(string statName)
        {
            (bool contains, Stat stat) findStat = _stats.FindContent(_ => _.Name == statName);
            return findStat;
        }

        [Inject]
        private void Init()
        {
            InitStats();
        }

        private void InitStats()
        {
            _data.statDatas.ForEach(data => _stats.Add(new Stat(data)));
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("CollisionEnter");
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.TakeDamage();
                _pool.Despawn(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("CollisionEnter2D");
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.TakeDamage();
                _pool.Despawn(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter");
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.TakeDamage();
                _pool.Despawn(this);
            }
        }

        #endregion

        #region Nested Types

        public class Factory : PlaceholderFactory<Bullet.SpawnData, TestBullet>
        {}

        #endregion
    }
}