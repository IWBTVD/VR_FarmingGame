using UnityEngine;

//todo: 모든 퀘스트관련 이벤트를 다 담아두고 퀘스트가 업데이트 될때마다 실행하도록 수정

namespace Jun
{
    public class BehaviourManager : MonoBehaviour
    {
        public static BehaviourManager instance;
        public static BehaviourManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BehaviourManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "BehaviourManager";
                        instance = obj.AddComponent<BehaviourManager>();
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// 땅 경작 횟수를 담을 변수
        /// </summary>
        int plowedCount = 0;

        /// <summary>
        /// 물 주기 횟수를 담을 변수
        /// </summary>
        int wateredCount = 0;

        /// <summary>
        /// 씨뿌리기 횟수를 담을 변수
        /// </summary>
        int sowedCount = 0;

        public void AddPlowedCount()
        {
            plowedCount++;
            if (QuestManager.instance.currentQuestID == 1)
                QuestManager.instance.CheckQuestbehaviour(GetComponent<Quest>().GetbehaviourID());
            Debug.Log("경작횟수 : " + plowedCount);
        }

        public void AddWateredCount()
        {
            wateredCount++;
            if (QuestManager.instance.currentQuestID == 2)
                QuestManager.instance.CheckQuestbehaviour(GetComponent<Quest>().GetbehaviourID());
            Debug.Log("물주기횟수 : " + wateredCount);
        }

        public void AddSowedCount()
        {
            sowedCount++;
            if (QuestManager.instance.currentQuestID == 3)
                QuestManager.instance.CheckQuestbehaviour(GetComponent<Quest>().GetbehaviourID());
            Debug.Log("씨뿌리기횟수 : " + sowedCount);
        }

    }
}