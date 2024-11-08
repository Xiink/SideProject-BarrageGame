using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Codice.Client.Commands.WkTree;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Events;
using Game.Scripts.Enemy.Handlers;
using Game.Scripts.Enemy.Main;
using Game.Scripts.Enemy.UI;
using Game.Scripts.Helpers;
using Game.Scripts.Names;
using Game.Scripts.RPG;
using PlasticGui.Configuration.CloudEdition.Welcome;
using rStarUtility.Generic.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Game.Scripts.Enemy
{
    /// <summary>
    /// TODO:目前可以透過工廠模式來讓每個Enemy有自己的EnemyData，但內部的DomainData還是沒有
    /// TODO:確認EnemyData中的DomainData及VisualData有沒有更好的設定方式
    /// </summary>

    // [ExecuteInEditMode]
    public class Enemy : MonoBehaviour, IMover, IPoolable<IMemoryPool>
    {
        #region Public Variables

        public IMemoryPool _pool;

        public bool Moveable => _moveable.GetState();

        [Inject]
        public EnemyData _data { get; set; }

        /// <summary>
        /// 敵人身上擁有的屬性
        /// </summary>
        public ReadOnlyCollection<Stat> Stats => _stats.Contents;

        [Inject(Id = "NormalEnemy")]
        public Rigidbody2D rigidbody2D { get; set; }

        public Transform trans { get; set; }

        public Transform Trans
        {
            get
            {
                if (trans == null) trans = transform;
                return trans;
            }
        }

        #endregion

        #region Private Variables

        [Inject]
        private IMoveable _moveable;

        [Inject(Id = "NormalEnemy")]
        private EnemyHpBar _enemyHpBarUIHandler;

        [Inject(Id = "NormalEnemy")]
        private SpriteRenderer _spriteRenderer;

        [Inject]
        private List<NormalEnemyDieObserver> _normalEnemyDieObserver;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Public Methods

        public void AddForce(Vector3 force)
        {
            // this.GetComponent<Rigidbody2D>().AddForce(force);
            rigidbody2D.AddForce(force);
        }

        [Inject]
        public void Construct(EnemyData data)
        {
            _spriteRenderer.sprite = _data._visualData._Sprite;
        }

        public void Die()
        {
            // 觸發死亡事件，一一呼叫有繼承INormalEnemyDieObserver的物件
            foreach (var e in _normalEnemyDieObserver)
            {
                e.OnNormalEnemyDie();
            }

            _pool.Despawn(this);
            // Destroy(gameObject);
        }

        public Vector2 GetPosition()
        {
            return (Vector2)Trans.position;
        }

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
        }

        public void OnSpawned(IMemoryPool p1)
        {
            _pool = p1;
            InitStats();
        }

        public void SetPosition(Vector2 newPos)
        {
            Trans.position = (Vector3)newPos;
        }

        public void SetStatAmount(string name, float value)
        {
            var (contains, stat) = _stats.FindContent(_ => _.Name == name);

            // 如果當前角色已有該屬性直接進行設定
            // 沒有的話則新增一個Stat
            if (contains)
            {
                stat.SetAmount(value);
            }
            else
            {
                _stats.Add(new Stat(new Stat.Data(name, value)));
            }
        }

        public void TakeDamage(float damage)
        {
            var newHp = CalculateHp(damage, out var percent);

            UpdateEnemyHpBarUI(percent);

            if (newHp <= 0)
            {
                Die();
            }
        }

        public float CalculateHp(float damage, out float percent)
        {
            // 讀取當前Stat
            var nowHp = GetStatFinalValue(StatNames.Hp);

            // 扣血
            nowHp -= damage;

            // 設定當前Stat
            SetStatAmount(StatNames.Hp, nowHp);

            // 取得更新後的Stat
            var newHp =  GetStatFinalValue(StatNames.Hp);

            percent = newHp / _data.maxhp;
            return newHp;
        }

        private void UpdateEnemyHpBarUI(float percent)
        {
            _enemyHpBarUIHandler.SetPercent(percent);

            _enemyHpBarUIHandler.DoFlash();
        }

        #endregion

        #region Private Methods

        private (bool containsStat, Stat stat) FindStat(string statName)
        {
            (bool contains, Stat stat) findStat = _stats.FindContent(_ => _.Name == statName);
            return findStat;
        }

        [Indent]
        private void Init()
        {
            InitStats();
        }

        public void InitStats()
        {
            _stats = new GenericRepository<Stat>();
            UpdateEnemyHpBarUI(100);
            _data._domaindata.Datas.ForEach(data => _stats.Add(new Stat(data)));
            // _stats.Add(new Stat(_data.hp));
        }

        #endregion

        #region Nested Types

        public class Factory : PlaceholderFactory<Enemy>
        {}

        #endregion

        private void Update()
        {
            Debug.Log(FindStat(StatNames.Hp).stat.Amount);
        }
    }
}