using System.Collections;
using System.Collections.Generic;
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

        void Update()
        {

        }

        public void Complete()
        {
            QuestManager.Instance.UpdateQuest(questID + 1);
        }
    }
}
