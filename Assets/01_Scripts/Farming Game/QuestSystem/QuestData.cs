using UnityEngine;

namespace Jun
{
    //questId,name,content,questTarget,reward,rewardObject,progressGoal,questType

    /// <summary>
    /// QuestData를 저장할 구조체
    /// </summary>
    public class QuestData
    {
        public int questID;
        public string name;
        public string content;
        // public int questState;
        // public int questGiver;
        public int questTarget;
        public int reward;
        public GameObject rewardObject;
        // public int questRewardCount;
        // public int questCurrentCount;
        // public int questGoalCount;
        internal int progressGoal;

        public int questType;
        // internal DayOfWeek resetDayOfWeek;
        // internal double resetHourUTC;

        public QuestData(int questID, string name, string content, int questTarget, int reward, GameObject rewardObject, int progressGoal, int questType)
        {
            this.questID = questID;
            this.name = name;
            this.content = content;
            this.questTarget = questTarget;
            this.reward = reward;
            this.rewardObject = rewardObject;
            this.progressGoal = progressGoal;
            this.questType = questType;
        }


    }
}