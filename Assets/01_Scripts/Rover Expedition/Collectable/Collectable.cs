using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoverExpedition
{

    public class Collectable : MonoBehaviour
    {
        public CollectableSO collectableSO;


        public void GetInfo()
        {

        }

        /// <summary>
        /// 채집 시도하기
        /// </summary>
        public virtual void TryCollect()
        {
            // Debug.Log(collectableSO.Description);
        }

        /// <summary>
        /// 채집 성공
        /// </summary>
        public virtual void OnCollected()
        {
            // Debug.Log(collectableSO.PriceValue);
        }


    }
}