using UnityEngine;
using TMPro;
using DG.Tweening;

namespace OleksiiStepanov.Skillshare.MVC
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelNumberText;
        [SerializeField] private TMP_Text messageText;

        public void Init(int levelNumber)
        {
            levelNumberText.text = $"{Constants.LEVEL_TEXT}{levelNumber}";

            levelNumberText.transform.DOScale(1.2f, 0.25f).OnComplete(()=> { levelNumberText.transform.DOScale(1f, 0.25f); });

            messageText.text = "";
        }

        public void SetMessageText(string text)
        {
            messageText.text = text;
        }
    }
}
