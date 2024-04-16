using UnityEngine;

namespace Jun
{
    public class QuestUI : MonoBehaviour
    {
        public bool IsUIOpen;
        public int QuestID;
        public TMPro.TextMeshProUGUI questTitle;
        public TMPro.TextMeshProUGUI questContent;
        void Start()
        {
            // CloseUI();
            QuestManager.Instance.Subscribe(Action);
        }

        public void Action()
        {
            QuestID = QuestManager.Instance.currentQuestID;
            QuestData questData = QuestManager.Instance.GetQuestData(QuestID);
            if (questData == null) return;
            SetQuestText(questData.name, questData.content);
        }

        public void SetQuestText(string name, string content)
        {
            Debug.Log("SetQuestText" + QuestID + " " + name + " " + content);

            questTitle.text = name;
            questContent.text = content;
        }

        public void OpenCloseUI()
        {
            IsUIOpen = !IsUIOpen;
            if (IsUIOpen)
            {
                OpenUI();
            }
            else
            {
                CloseUI();
            }
        }

        private void OpenUI()
        {
            gameObject.SetActive(true);
        }

        private void CloseUI()
        {
            gameObject.SetActive(false);
        }
    }
}
