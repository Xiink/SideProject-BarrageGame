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

        public void SetPercent(float percent)
        {
            // 0 ~ 1
            forntBar.fillAmount = percent;
            spriteRenderer.material = flashMaterial;

            // delay call
            Observable.Timer(TimeSpan.FromSeconds(delayEffectDuration)).Subscribe(_ =>
            {
                deltaBar.DOFillAmount(forntBar.fillAmount, 0.3f).SetEase(Ease.InOutCubic);
                spriteRenderer.material = normalMaterial;
                // deltaBar.fillAmount =  forntBar.fillAmount;
            }).AddTo(gameObject);
        }
    }
}