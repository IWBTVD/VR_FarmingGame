using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class Item : MonoBehaviour
    {

        /// <summary>
        /// 판매가 가능한지 판단
        /// </summary>
        public bool isSellable = true;
        public int price = 100;

        public enum ItemType
        {
            carrot,
            apple,
            orange,
            banana,
            corn,
        }

        public ItemType itemType;
        // enum아이템을 2개로 관리
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public new string GetType()
        {
            // Debug.Log("Item Type : " + itemType.name);
            return itemType.ToString();
        }
    }
}