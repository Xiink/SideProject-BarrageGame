using System;
using System.Collections.Generic;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Enemy.Data;
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

        public bool Moveable { get; }

        public Rigidbody rigidbody { get; }
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

        public SpriteRenderer sprit;

        #region Private Variables

        [Inject]
        public EnemyData _data;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Public Methods

        //private void Awake()
        //{
        //    Debug.Log(testint);
        //}

        public void Die()
        {
            Destroy(gameObject);
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
            // this.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color",_data.ColorOptions);
            InitStats();
        }

        private void InitStats()
        {
            // Debug.Log(_data.Behaviour.PhaseDataList[0].Data.GetType());
            // _data.statDatas.ForEach(data => _stats.Add(new Stat(data)));
        }

        #endregion

        #region Nested Types

        private void Update()
        {

        }
        // [Inject]
        // private void Update()
        // {
        //     UpdateData();
        // }
        //
        // [Inject]
        // private void UpdateData()
        // {
        //     // Debug.Log(_data);
        //     this.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color",_data.ColorOptions);
        // }

        #endregion

    }
}