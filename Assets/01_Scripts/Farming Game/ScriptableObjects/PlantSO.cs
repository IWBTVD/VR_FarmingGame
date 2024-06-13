using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    [CreateAssetMenu(fileName= "Plant SO", menuName ="농장게임 ScriptableObject/새로운 식물 SO 생성")]
    public class PlantSO : ScriptableObject
    {
        [SerializeField] private string plantName;
        [SerializeField, TextArea] private string description;
        [SerializeField] private int maxGrowthDay;

        public string PlantName => plantName;
        public string Description => description;
        /// <summary>
        /// 최대로 성장할 때까지 걸리는 일 수
        /// </summary>
        public int MaxGrowthDay => maxGrowthDay;
    }
}

