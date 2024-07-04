using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoverExpedition
{
    [CreateAssetMenu(fileName = "Collectable SO", menuName = "우주차량대탐험 SO/Collectable SO 생성")]
    public class CollectableSO : ScriptableObject
    {
        /// <summary>
        /// 해당 자원의 타입 
        /// </summary>
        public enum CollectType
        {
            Plant
        }

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _priceValue;
        [SerializeField] private int _weight;

        /// <summary>
        /// 이름
        /// </summary>
        public string Name => _name;
        /// <summary>
        /// 설명
        /// </summary>
        public string Description => _description;
        /// <summary>
        /// 판매 가격
        /// </summary>
        public int PriceValue => _priceValue;
        /// <summary>
        /// 무게
        /// </summary>
        public int Weight => _weight;
    }
}