using System;
using System.Collections.Generic;
using Game.Scripts.Bullet.Interfaces;
using Game.Scripts.Helpers;
using Game.Scripts.Names;
using Game.Scripts.RPG;
using rStarUtility.Generic.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using VInspector.Libs;
using Zenject;
using ITimeProvider = Game.Scripts.Battle.Misc.ITimeProvider; // 要使用我自己創的ITimeProvider

namespace Game.Scripts.Bullet
{
    public enum BulletTypes
    {
        FromEnemy,
        FromPlayer,
        Other
    }

    /// <summary>
    /// 繼承一個IBullet介面，並且實作這個介面
    /// 有新的Bullet同樣繼承這個介面來讓Factory可以生產
    /// </summary>
    public class Bullet : MonoBehaviour, IBullet
    {
        #region Public Variables

        public IMemoryPool _pool;
        public BulletTypes _type;

        #endregion

        #region Private Variables

        float _startTime;

        // 從Prefab上的GameObjectContext進行注入
        [Inject] private Bullet.Data _data;

        [Inject] private ITimeProvider _timeProvider;

        private GenericRepository<Stat> _basestats = new GenericRepository<Stat>();

        #endregion

        #region Unity events

        private void Update()
        {
            transform.Translate(FindStat(StatNames.MoveSpeed).stat.Amount * _timeProvider.GetDeltaTime() * -1, 0, 0);

            var _lifeTime = GetStatFinalValue(StatNames.BulletLifeTime);

            if (_timeProvider.GetRealtimeSinceStartup() - _startTime > _lifeTime)
            {
                Destroy(gameObject);
                // _pool.Despawn(this);
                // Destroy(this);
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
            _type = data.Type;
            _pool = p3;

            _startTime = _timeProvider.GetRealtimeSinceStartup();

            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        public void OnSpawned(Bullet.SpawnData data)
        {
            _startTime = _timeProvider.GetRealtimeSinceStartup();

            _type = data.Type;
            transform.position = data.Position;
            transform.rotation = data.Rotation;
        }

        #endregion

        #region Private Methods

        private (bool containsStat, Stat stat) FindStat(string statName)
        {
            (bool contains, Stat stat) findStat = _basestats.FindContent(_ => _.Name == statName);
            return findStat;
        }

        [Inject]
        private void Init()
        {
            InitStats();
        }

        private void InitStats()
        {
            _data.statDatas.ForEach(data => _basestats.Add(new Stat(data)));

            SetVisualStat();
        }

        private void SetVisualStat()
        {
            var size = GetStatFinalValue(StatNames.BulletSize);
            transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y * size, transform.localScale.z);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.GetComponent<Enemy.Enemy>();
            if (enemy != null && _type == BulletTypes.FromPlayer)
            {
                var damage = FindStat(StatNames.Atk).stat.Amount;
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                // _pool.Despawn(this);
            }
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

        /// <summary>
        /// Zenject Factory
        /// </summary>
        public class Factory : PlaceholderFactory<Bullet>
        {
        }

        public class SpawnData
        {
            #region Public Variables

            public BulletTypes Type;

            public Quaternion Rotation;

            public Vector3 Position;

            #endregion

            #region Constructor

            public SpawnData(BulletTypes type, Vector3 position, Quaternion rotation)
            {
                Type = type;
                Position = position;
                Rotation = rotation;
            }

            #endregion
        }

        #endregion
    }
}