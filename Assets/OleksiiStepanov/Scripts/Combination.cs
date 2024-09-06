using UnityEngine;
using System;
using System.Collections.Generic;

namespace OleksiiStepanov.Skillshare.MVC
{
    [CreateAssetMenu(menuName = "OleksiiStepanov/New Combination", fileName = "New Combination")]
    public class Combination : ScriptableObject
    {
        [Header("Possible keys")]

        [SerializeField] private List<KeyData> keys;

        public List<KeyData> keyCombination { get; private set; }

        public void CreateCombination()
        {
            keyCombination.Clear();

            int combinationSize = UnityEngine.Random.Range(Constants.MIN_KEY_AMOUNT, Constants.MAX_KEY_AMOUNT);

            for (int i = 0; i < combinationSize; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, keys.Count);
                keyCombination.Add(keys[randomIndex]);
            }
        }

        public bool AreKeysAssigned()
        {
            return keys.Count > 0;
        }
    }

    [Serializable]
    public class KeyData
    {
        public Sprite sprite;
        public KeyCode KeyCode;
    }
}
