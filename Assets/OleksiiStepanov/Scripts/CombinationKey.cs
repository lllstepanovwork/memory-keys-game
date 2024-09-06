using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OleksiiStepanov.Skillshare.MVC
{
    public class CombinationViewKey : MonoBehaviour
    {
        [SerializeField] private Image keyImage;

        private KeyData keyData;

        public bool Active { get; private set; } = false;

        public void Init(KeyData keyData)
        {
            Active = true;

            this.keyData = keyData;

            keyImage.sprite = keyData.sprite;
            keyImage.color = Color.white;

            gameObject.SetActive(true);
        }

        public void ResetState()
        {
            Active = false;

            gameObject.SetActive(false);
        }

        public bool IsKeyCodeCorrect(KeyCode keyCode)
        {
            return keyCode == keyData.KeyCode;
        }

        public void SetAsCorrect()
        {
            keyImage.color = Color.yellow;
            keyImage.transform.DOScale(1.2f, 0.25f).OnComplete(() => { keyImage.transform.DOScale(1f, 0.25f); });
        }

        public void SetAsIncorrect()
        {
            keyImage.color = Color.red;
            keyImage.transform.DOScale(1.2f, 0.25f).OnComplete(() => { keyImage.transform.DOScale(1f, 0.25f); });
        }

        public void SetAsHidden()
        {
            keyImage.color = Color.clear;
        }

        public void SetAsExposed()
        {
            keyImage.color = Color.white;
        }
    }
}
