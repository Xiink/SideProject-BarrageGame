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
    [ExecuteInEditMode]
    public class Enemy : MonoBehaviour,IMover
    {
        #region Public Variables
        public Rigidbody rigidbody { get; }

        private Rigidbody2D rigi2D => GetComponent<Rigidbody2D>();

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

        public bool Moveable => _moveable.GetState();

        // public SpriteRenderer sprit;

        #region Private Variables

        // [Inject]
        // public EnemyData _data;

        [Inject]
        public EnemyStateManager _EnemyStateManager;

        [Inject]
        private IMoveable _moveable;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Public Methods

        public void Die()
        {
            Destroy(gameObject);
        }

        public Vector2 GetPosition()
        {
            return (Vector2)Trans.position;
        }

        public void AddForce(Vector3 force)
        {
            rigi2D.AddForce(force);
        }

        public float GetStatFinalValue(string statName)
        {
            return 0;
        }

        public void SetPosition(Vector2 newPos)
        {
            Trans.position = (Vector3)newPos;
        }

        // public void SetStatAmount(string name, float value)
        // {
        //     var (contains, stat) = _stats.FindContent(_ => _.Name == name);
        //
        //     // 如果當前角色已有該屬性直接進行設定
        //     // 沒有的話則新增一個Stat
        //     if (contains)
        //     {
        //         stat.SetAmount(value);
        //     }
        //     else
        //     {
        //         _stats.Add(new Stat(new Stat.Data(name, value)));
        //     }
        // }

        #endregion

        // #region Private Methods
        //
        // private (bool containsStat, Stat stat) FindStat(string statName)
        // {
        //     (bool contains, Stat stat) findStat = _stats.FindContent(_ => _.Name == statName);
        //     return findStat;
        // }
        //
        // private void InitStats()
        // {
        //
        // }
        //
        // #endregion

        // [Serializable]
        // public class Data
        // {
        //
        // }
    }
}