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

        public int testint => _data.value;

        #region Private Variables

        [Inject]
        public EnemyData _data { get; private set; }

        private GenericRepository<Stat> _stats = new GenericRepository<Stat>();

        #endregion

        #region Public Methods

        private void Awake()
        {
            Debug.Log(testint);
        }

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
            // _data.statDatas.ForEach(data => _stats.Add(new Stat(data)));
        }

        #endregion

        #region Nested Types

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

        [Serializable]
        public class Data
        {
            [PreviewField]
            public Sprite sprite;
        }
        // {
            // #region Public Variables
            // [ValidateInput(nameof(StatDataValidation), ContinuousValidationCheck = true)]
            // public List<Stat.Data> statDatas = new List<Stat.Data>();
            //
            // public Material _Material;
            //
            // #endregion
            //
            // #region Private Methods
            //
            // private bool StatDataValidation(List<Stat.Data> datas, ref string errorMessage)
            // {
            //     return ValidationHelper.StatDataValidation(datas, ref errorMessage);
            // }

            // #endregion

            // [HorizontalGroup("Split")]
            // [FoldoutGroup("Split/Domain Data")]
            // [LabelWidth(100)]
            // public float Life;
            // [FoldoutGroup("Split/Domain Data")]
            // [LabelWidth(100)]
            // public float MP;
            // [FoldoutGroup("Split/Domain Data")]
            // [LabelWidth(100)]
            // public float Offset;
            //
            // [LabelText("Size")]
            // [HorizontalGroup("Split/Domain Data/Group 1", LabelWidth = 20)]
            // public Label Size ;
            // [HorizontalGroup("Split/Domain Data/Group 1")]
            // public float x;
            // [HorizontalGroup("Split/Domain Data/Group 1")]
            // public float y;
            //
            // [FoldoutGroup("Split/Visual Data")]
            // public string DisplayName;
            // [FoldoutGroup("Split/Visual Data")]
            // public Material EnemyMaterial;
            //
            // [FoldoutGroup("Behaviour")]
            // [ValueDropdown("@GetAllPhases.GetAllPhaseNames", ExpandAllMenuItems = true,
            //     IsUniqueList = true,
            //     DropdownHeight = 250, DropdownWidth = 300)]
            // public string Behaviour;
        // }

        #endregion

    }
}