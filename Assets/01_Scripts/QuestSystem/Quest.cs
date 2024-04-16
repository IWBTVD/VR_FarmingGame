using UnityEngine;
using System;
using System.Collections.Generic;

namespace Jun
{
    public enum QuestType
    {
        None,
        Daily,
        Weekly,
        Achievement
    }
    // public class Quest : MonoBehaviour
    // {
    //     public QuestData questData;

    //     public int progress;
    //     public bool isCompleted;
    //     public DateTime nextResetTimeUTC; // 다음 초기화 시간 추가


    //     public Quest(QuestData questData)
    //     {
    //         this.questData = questData;
    //         progress = 0;
    //         isCompleted = false;
    //     }

    //     public void UpdateProgress(int amount)
    //     {
    //         if (!isCompleted)
    //         {
    //             progress += amount;
    //             if (progress >= questData.progressGoal)
    //             {
    //                 isCompleted = true;
    //             }
    //         }
    //     }

    //     public bool IsTimeLimitExceeded()
    //     {
    //         return DateTime.UtcNow > nextResetTimeUTC;
    //     }

    //     public void Reset()
    //     {
    //         progress = 0;
    //         isCompleted = false;

    //         // 다음 초기화 시간 설정
    //         if (questData.questType == QuestType.Daily)
    //         {
    //             nextResetTimeUTC = nextResetTimeUTC.AddDays(1);
    //         }
    //         else if (questData.questType == QuestType.Weekly)
    //         {
    //             nextResetTimeUTC = nextResetTimeUTC.AddDays(7);
    //         }
    //     }

    // }
}