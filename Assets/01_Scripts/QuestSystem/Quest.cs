#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Jun
{
    public class Quest : MonoBehaviour
    {
        public enum ItemType
        {
            Item,
            Visit,
            Behavior
        }
        public ItemType itemType;

        [SerializeField]
        private int itemID;

        [SerializeField]
        private int locationID;

#if UNITY_EDITOR
        [CustomEditor(typeof(Quest))]
        public class QuestEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                Quest questItem = (Quest)target;

                EditorGUILayout.PropertyField(serializedObject.FindProperty("itemType"));

                if (questItem.itemType == ItemType.Item)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("itemID"));
                }
                else if (questItem.itemType == ItemType.Visit)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("locationID"));
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        void Start()
        {
            QuestManager.Instance.questItemList.Add(this);
        }

        public void Complete()
        {
            QuestManager.Instance.UpdateQuest();
        }

        public int GetItemID()
        {
            return itemID;
        }

        public int GetLocationID()
        {
            return locationID;
        }
    }
}
