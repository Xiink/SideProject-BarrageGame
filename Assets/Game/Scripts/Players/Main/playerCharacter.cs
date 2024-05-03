using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Scripts.Battle.Misc;
using Game.Scripts.Helpers;
using Game.Scripts.RPG;
using rStarUtility.Generic.Infrastructure;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Players.Main
{
    public class PlayerCharacter : MonoBehaviour, IMover
    {
        #region Public Variables

        [Inject] private IMoveable _moveable;

        public bool Moveable => _moveable.GetState();

        /// <summary>
        /// 角色身上擁有的屬性
        /// </summary>
        public ReadOnlyCollection<Stat> Stats => _stats.Contents;

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

        [Inject] private Data _data;

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Public Methods

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

        #endregion
    }
}