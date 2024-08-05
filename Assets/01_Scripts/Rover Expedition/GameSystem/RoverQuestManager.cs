using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jun
{
    public class RoverQuestManager : MonoBehaviour
    {
        public static RoverQuestManager instance;
        public static RoverQuestManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<RoverQuestManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "QuestManager";
                        instance = obj.AddComponent<RoverQuestManager>();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// QuestData.csv 파일을 저장할 변수
        /// </summary>
        [SerializeField, Required] TextAsset questDataCSV;
        /// <summary>
        /// QuestData를 저장할 리스트
        /// </summary>
        public List<RoverQuestData> questDataList = new List<RoverQuestData>();
        /// <summary>
        /// QuestItem을 저장할 리스트
        /// </summary>
        // public List<QuestItem> questItemList = new List<QuestItem>();

        public List<Quest> questItemIDList = new List<Quest>();
        public List<Quest> questLocationIDList = new List<Quest>();
        public List<Quest> questBehaviourIDList = new List<Quest>();

        /// <summary>
        /// Quest가 업데이트 될 때 실행할 이벤트
        /// </summary>
        public UnityEvent OnQuestUpdated;

        /// <summary>
        /// 현재 진행중인 Quest ID
        /// </summary>
        public int currentQuestID = 0;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadQuestData();
            OnQuestUpdated.Invoke();
        }

        void Update()
        {
            TestCode();
        }

        private void TestCode()
        {
            // 테스트용 코드
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UpdateQuest();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                for (int i = 0; i < questDataList.Count; i++)
                {
                    Debug.Log(questDataList[i].questID + " " + questDataList[i].name + " " + questDataList[i].content + " " + questDataList[i].questTarget + " " + questDataList[i].reward + " " + questDataList[i].rewardObject + " " + questDataList[i].progressGoal + " " + questDataList[i].questType);
                }
            }
        }

        public void Subscribe(UnityAction action)
        {
            OnQuestUpdated.AddListener(action);
        }

        public void UpdateQuest()
        {
            currentQuestID++;
            OnQuestUpdated.Invoke();
        }

        /// <summary>
        /// QuestData.csv 파일을 읽어와서 QuestData를 생성하는 함수
        /// </summary>
        private void LoadQuestData()
        {
            string[] data = questDataCSV.text.Split(new char[] { '\n' });

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] == "") return;
                string[] row = data[i].Split(new char[] { ',' });
                int questID = int.Parse(row[0]);
                string name = row[1];
                string content = row[2];
                int questTarget = int.Parse(row[3]);
                int reward = int.Parse(row[4]);
                GameObject rewardObject = Resources.Load<GameObject>(row[5]);//수정해야함 아이템에 코드를 부여여 해야함
                int progressGoal = int.Parse(row[6]);
                int questType = int.Parse(row[7]);

                RoverQuestData questData = new RoverQuestData(questID, name, content, questTarget, reward, rewardObject, progressGoal, questType);
                questDataList.Add(questData);
            }
        }

        /// <summary>
        /// QuestData를 추가하는 함수
        /// </summary>
        /// <param name="questData"></param>
        public void AddQuestData(RoverQuestData questData)
        {
            questDataList.Add(questData);
        }

        public void RemoveQuestData(RoverQuestData questData)
        {
            questDataList.Remove(questData);
        }

        /// <summary>
        /// QuestItem을 체크하는 함수 현재 잡고 있는게 퀘스트에 맞는 아이템인지 확인
        /// </summary>
        /// <param name="questID"></param>
        public void CheckQuestItem(int questID)
        {
            for (int i = 0; i < questItemIDList.Count; i++)
            {
                if (questItemIDList[i].GetItemID() == questDataList[currentQuestID].questTarget)
                {
                    UpdateQuest();
                }
            }
        }

        /// <summary>
        /// QuestLocation을 체크하는 함수
        /// </summary>
        /// <param name="questID"></param>

        public void CheckQuestLocation(int questID)
        {
            for (int i = 0; i < questLocationIDList.Count; i++)
            {
                if (questLocationIDList[i].GetLocationID() == questDataList[currentQuestID].questTarget)
                {
                    UpdateQuest();
                }
            }
        }

        /// <summary>
        /// Questbehaviour를 체크하는 함수
        /// </summary>
        /// <param name="questID"></param>
        public void CheckQuestbehaviour(int questID)
        {
            for (int i = 0; i < questBehaviourIDList.Count; i++)
            {
                if (questBehaviourIDList[i].GetbehaviourID() == questDataList[currentQuestID].questTarget)
                {
                    UpdateQuest();
                }
            }
        }

        /// <summary>
        /// QuestData를 가져오는 함수
        /// </summary>
        /// <param name="questID"></param>
        /// <returns></returns>
        public RoverQuestData GetQuestData(int questID)
        {
            for (int i = 0; i < questDataList.Count; i++)
            {
                if (questDataList[i].questID == questID)
                {
                    return questDataList[i];
                }
            }
            return null;
        }

    }
}