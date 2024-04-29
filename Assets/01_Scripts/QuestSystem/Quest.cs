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
            behaviour
        }
        public ItemType itemType;

        [SerializeField]
        private int itemID;

        [SerializeField]
        private int locationID;

        [SerializeField]
        private int behaviourID;

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
                else
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("behaviourID"));
                }

                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        void Start()
        {
            if (itemType == ItemType.Item)
            {
                QuestManager.Instance.questItemList.Add(this);
            }
            else if (itemType == ItemType.Visit)
            {
                QuestManager.Instance.questLocationList.Add(this);
            }
            else
            {
                QuestManager.Instance.questBehaviourList.Add(this);
            }
        }

        public void UnableIsKinematic()
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        public void AbleIsKinematic()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        public int GetItemID()
        {
            return itemID;
        }

        public int GetLocationID()
        {
            return locationID;
        }

        public int GetbehaviourID()
        {
            return behaviourID;
        }
    }
}
