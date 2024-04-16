#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Jun
{
    //안쓸예정
    public class QuestItem : MonoBehaviour
    {
        enum ItemType
        {
            Item,
            location
        }
        public int questID;
        public int itemType;

        [SerializeField]
        private int yourVariable;

#if UNITY_EDITOR
        [CustomEditor(typeof(QuestItem))]
        public class QuestItemEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                QuestItem questItem = (QuestItem)target;

                EditorGUILayout.PropertyField(serializedObject.FindProperty("questID"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("itemType"));

                // 아이템 타입이 1일 때만 yourVariable 표시
                if (questItem.itemType == 1)
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("yourVariable"));
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        void Start()
        {

        }

        public void Complete()
        {
            QuestManager.Instance.UpdateQuest();
        }
    }
}
