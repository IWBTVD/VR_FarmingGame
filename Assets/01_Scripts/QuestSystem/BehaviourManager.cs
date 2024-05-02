using UnityEngine;

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

        /// <summary>
        /// 파괴한 돌의 갯수를 담을 변수
        /// </summary>
        int destroyedRockCount = 0;

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

        public void AddDestroyedRockCount()
        {
            destroyedRockCount++;
            if (QuestManager.instance.currentQuestID == 4)// Doto: 돌파괴 퀘스트 ID를 만들어야함
                QuestManager.instance.CheckQuestbehaviour(GetComponent<Quest>().GetbehaviourID());
            Debug.Log("돌파괴횟수 : " + destroyedRockCount);
        }

    }
}