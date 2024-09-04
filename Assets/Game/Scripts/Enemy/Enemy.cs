using System;
using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
using Game.Scripts.Enemy.Main;
using Game.Scripts.Helpers;
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
    /// TODO:目前是注入EnemyData，但是會有多個同樣敵人會注入同樣EnemyData導致敵人共用同樣數值的問題
    /// TODO:需要改成每個敵人都有自己的EnemyData
    /// TODD:使用物件池來產生Enemy
    /// TODO:確認EnemyData中的DomainData及VisualData有沒有更好的設定方式
    /// </summary>

    [ExecuteInEditMode]
    public class Enemy : MonoBehaviour,IMover
    {
        #region Public Variables

        public bool Moveable => _moveable.GetState();

        public Transform trans { get; set; }

        public Transform Trans
        {
            get
            {
                if (trans == null) trans = transform;
                return trans;
            }
        }

        [Inject]
        public EnemyData _data;

        #endregion

        #region Private Variables

        [Inject]
        private IMoveable _moveable;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        [Inject]
        public Rigidbody2D rigidbody2D { get; set; }
        // private Rigidbody2D rigi2D => GetComponent<Rigidbody2D>();

        #endregion

        #region Public Methods

        public void AddForce(Vector3 force)
        {
            rigidbody2D.AddForce(force);
        }

        public void Die()
        {
            _data._domaindata.life -= 1;
            // Destroy(gameObject);
        }

        public Vector2 GetPosition()
        {
            return (Vector2)Trans.position;
        }

        public float GetStatFinalValue(string statName)
        {
            return 0;
        }

        public void SetPosition(Vector2 newPos)
        {
            Trans.position = (Vector3)newPos;
        }
        
        public class Factory : PlaceholderFactory<Enemy>
        {
            
        }

        #endregion

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

        #region Private Methods

        private (bool containsStat, Stat stat) FindStat(string statName)
        {
            (bool contains, Stat stat) findStat = _stats.FindContent(_ => _.Name == statName);
            return findStat;
        }

        private void InitStats()
        {

        }

        #endregion
    }
}