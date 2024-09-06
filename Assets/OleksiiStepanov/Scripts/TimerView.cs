using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OleksiiStepanov.Skillshare.MVC
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Image barImage;

        [Range(6, 12)] [SerializeField] private int timerTime;
        
        public static Action OnTimerElapsed;

        private Tweener tweener;

        public void StartTimer(int keyAmount)
        {
            StopTimer();

            int timerTime = Mathf.Clamp(keyAmount + 2, 5, 10);

            tweener = barImage.DOFillAmount(1, timerTime).OnComplete(() => OnTimerElapsed?.Invoke()).SetEase(Ease.Linear);
        }

        private void StopTimer()
        {
            tweener.Kill();
            barImage.fillAmount = 0;
        }
    }
}
