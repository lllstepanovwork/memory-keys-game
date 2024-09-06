using UnityEditor;

namespace OleksiiStepanov.Skillshare.MVC
{
    [CustomEditor (typeof(Combination))]
    public class CombinationEditor : Editor
    {
        private Combination combination;

        public void OnEnable()
        {
            combination = (Combination)target;
        }

        public override void OnInspectorGUI()
        {
            if (!combination.AreKeysAssigned())
            {
                EditorGUILayout.HelpBox("Keys List is empty!", MessageType.Error);
            }

            base.OnInspectorGUI();

            EditorUtility.SetDirty(combination);
        }
    }
}
