using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Enemy.UI
{
    public class EnemyHpBar : MonoBehaviour
    {
        [SerializeField]
        [Required]
        private Image deltaBar;

        [SerializeField]
        [Required]
        private Image forntBar;

        [SerializeField]
        [Required]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        [Required]
        private Material normalMaterial;

        [SerializeField]
        [Required]
        private Material flashMaterial;

        [SerializeField]
        private float delayEffectDuration = 0.5f;

        [SerializeField]
        private float delayFlash = 0.2f;

        private bool flashEffectExecuting = false;

        public void Init()
        {
            // 重置進度條
            SetPercent(100);
        }

        public void SetPercent(float percent)
        {
            // 0 ~ 1
            forntBar.fillAmount = percent;

            // delay call
            Observable.Timer(TimeSpan.FromSeconds(delayEffectDuration)).Subscribe(_ =>
            {
                deltaBar.DOFillAmount(forntBar.fillAmount, 0.3f).SetEase(Ease.InOutCubic);
                // deltaBar.fillAmount =  forntBar.fillAmount;
            }).AddTo(gameObject);
        }

        public void DoFlash()
        {
            spriteRenderer.material = flashMaterial;
            if(flashEffectExecuting) return;
            flashEffectExecuting = true;
            Observable.Timer(TimeSpan.FromSeconds(delayFlash))
                .Subscribe(_ => { SetNormalMaterial(); })
                .AddTo(gameObject);
        }

        private void SetNormalMaterial()
        {
            spriteRenderer.material = normalMaterial;
            flashEffectExecuting = false;
        }
    }
}