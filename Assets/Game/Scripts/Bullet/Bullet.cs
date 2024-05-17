using System;
using System.Collections.Generic;
using Game.Scripts.Helpers;
using Game.Scripts.Names;
using Game.Scripts.RPG;
using rStarUtility.Generic.Infrastructure;
using Sirenix.OdinInspector;
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

    public class Bullet : MonoBehaviour, IPoolable<BulletTypes, IMemoryPool>
    {
        #region Public Variables

        public IMemoryPool _pool;
        public BulletTypes _type;
        // public float speed = 50f;

        #endregion

        #region Private Variables

        [Inject] private Bullet.Data _data;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Unity events

        private void Update()
        {
            transform.Translate(FindStat(StatNames.MoveSpeed).stat.Amount * Time.deltaTime * -1, 0, 0);
            Destroy(gameObject, 3);
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

        public void OnSpawned(BulletTypes type, IMemoryPool p3)
        {
            //speed = speed;
            _type = type;
            _pool = p3;
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter");
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.Die();
                _pool.Despawn(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter");
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.Die();
                _pool.Despawn(this);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("CollisionEnter");
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.Die();
                _pool.Despawn(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("CollisionEnter2D");
            var enemy = other.gameObject.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                enemy.Die();
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

        #endregion

        #region Nested Types

        [Serializable]
        public class Data
        {
            #region Public Variables

            [ValidateInput(nameof(StatDataValidation), ContinuousValidationCheck = true)]
            public List<Stat.Data> statDatas = new List<Stat.Data>();

            #endregion

            #region Private Methods

            private bool StatDataValidation(List<Stat.Data> datas, ref string errorMessage)
            {
                return ValidationHelper.StatDataValidation(datas, ref errorMessage);
            }

            #endregion
        }

        public class Factory : PlaceholderFactory<BulletTypes, Bullet>
        {}

        #endregion
    }
}