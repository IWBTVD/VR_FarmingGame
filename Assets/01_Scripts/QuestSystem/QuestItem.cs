using UnityEngine;

namespace Jun
{
    public class QuestItem : MonoBehaviour
    {
        public int questID;
        void Start()
        {
            QuestManager.Instance.questItemList.Add(this);
        }

        public void Complete()
        {
            QuestManager.Instance.UpdateQuest();
        }
    }
}
