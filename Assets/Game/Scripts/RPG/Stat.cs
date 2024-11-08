using System;
using Game.Scripts.Helpers;
using Game.Scripts.Values;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Game.Scripts.RPG
{
    public class Stat
    {
        #region Public Variables

        public float Amount { get; private set; }

        public string Name { get; }

        private readonly float min;

        private readonly float max;

        #endregion

        #region Constructor

        public Stat(Data data)
        {
            min = data.MinValue;
            max = data.MaxValue;

            this.Name = data.Name;
            SetAmount(data.Amount);
        }

        #endregion

        #region Public Methods

        public void SetAmount(float newAmount)
        {
            var clampValue = Math.Clamp(newAmount, min, max);
            Amount = clampValue;
        }

        #endregion

        #region Nested Types

        [Serializable]
        public class Data
        {
            #region Public Variables

            public float Amount => amount;

            // 此處會根據Stat名稱，取得該Stat的最大最小值
            public virtual float MaxValue => StatMinMaxValues.GetMax(Name);
            public virtual float MinValue => StatMinMaxValues.GetMin(Name);
            public string Name => name;

            #endregion

            #region Private Variables

            private readonly float min;

            private readonly float max;

            private string MaxValueInfo => StatMinMaxValues.GEtMaxInfo(Name);
            private string MinValueInfo => StatMinMaxValues.GEtMinInfo(Name, MinValue);

            private string MinMaxInfo => $"最小值：{MinValueInfo}\n最大值：{MaxValueInfo}";
            private string AmountSuffix => $"{MinValueInfo} ~ {MaxValueInfo}";

            [SerializeField]
            // [ValidateInput("@ValidationHelper.ValidateString(this.name)" ,defaultMessage:"Name is Empty")]
            [ValidateInput(nameof(ValidateName), defaultMessage: "Name is Empty")]
            [LabelText("名稱 :")]
            [HorizontalGroup("StatData")]
            [LabelWidth(30)]
            [ValueDropdown("@DropDownList.GetAllStatNames", DropdownTitle = "數值列表", ExpandAllMenuItems = true,
                IsUniqueList = true,
                DropdownHeight = 250, DropdownWidth = 300)]
            private string name;
            
            [MinValue("@MinValue")]
            [MaxValue("@MaxValue")]
            [SuffixLabel("@AmountSuffix", overlay: true, Icon = SdfIconType.ShieldFillExclamation)]
            // [OnValueChanged("OnAmountChanged")]
            [OnValueChanged(nameof(OnAmountChanged))]
            [LabelText("數值 : ")]
            [SerializeField]
            [ValidateInput("MinMaxValidation", defaultMessage: "最大值不能小於最小值")]
            [HorizontalGroup("StatData")]
            [LabelWidth(30)]
            private float amount;

            #endregion

            #region Constructor

            protected Data()
            {
            }

            public Data(string name, float amount)
            {
                SetName(name);
                SetAmount(amount);
            }

            #endregion

            #region Private Methods

            /// <summary>
            /// 驗證Name是否正常
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            private static bool ValidateName(string name)
            {
                return ValidationHelper.ValidateString(name);
            }

            protected void SetAmount(float newAmount)
            {
                var min = MinValue;
                var max = MaxValue;

                amount = Math.Clamp(newAmount, min, max);
            }

            protected void SetName(string name)
            {
                UnityEngine.Assertions.Assert.IsTrue(string.IsNullOrEmpty(name) == false,
                    message: "Name is null or Empty");

                this.name = name;
            }

            private bool MinMaxValidation()
            {
                return MaxValue > MinValue;
            }

            private void OnAmountChanged(float newAmount)
            {
                SetAmount(newAmount);
            }

            #endregion
        }

        #endregion
    }
}