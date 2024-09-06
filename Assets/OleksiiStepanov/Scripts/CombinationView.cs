using UnityEngine;
using System.Collections.Generic;

namespace OleksiiStepanov.Skillshare.MVC
{
    public class CombinationView : MonoBehaviour
    {
        [SerializeField] private List<CombinationViewKey> combinationKeys;

        private int currentCombinationKeyIndex = 0;

        public void Init(Combination combination)
        {
            currentCombinationKeyIndex = 0;

            foreach (CombinationViewKey combinationKey in combinationKeys)
            {
                combinationKey.ResetState();
            }

            for (int i = 0; i < combination.keyCombination.Count; i++)
            {
                if (i < combinationKeys.Count)
                {
                    combinationKeys[i].Init(combination.keyCombination[i]);
                }
            }
        }

        public void HideCombination()
        {
            currentCombinationKeyIndex = 0;

            foreach (CombinationViewKey combinationKey in combinationKeys)
            {
                if (combinationKey.Active)
                    combinationKey.SetAsHidden();
            }
        }

        public void ShowCombination()
        {
            foreach (CombinationViewKey combinationKey in combinationKeys)
            {
                if (combinationKey.Active)
                    combinationKey.SetAsExposed();
            }
        }

        public bool TryToSetCombinationKeyAsCorrect(KeyCode keyCode)
        {
            bool result = combinationKeys[currentCombinationKeyIndex].IsKeyCodeCorrect(keyCode);

            if (result)
            {
                combinationKeys[currentCombinationKeyIndex].SetAsCorrect();
                currentCombinationKeyIndex++;
            }
            else
            {
                combinationKeys[currentCombinationKeyIndex].SetAsIncorrect();
            }

            return result;
        }
    }
}
