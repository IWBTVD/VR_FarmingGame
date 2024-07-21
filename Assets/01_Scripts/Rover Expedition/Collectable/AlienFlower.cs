using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoverExpedition
{
    /// <summary>
    /// 수집 가능한 꽃
    /// </summary>
    public class AlienFlower : Collectable
    {

        public bool isColleted;

        public override void TryCollect()
        {
            base.TryCollect();
            if (isColleted == true)
            {
                OnCollected();
            }
            else
            {
                Debug.Log("AlienFlower is not Colleted");
            }
        }

        public override void OnCollected()
        {
            base.OnCollected();

            Debug.Log(collectableSO.PriceValue);
            Destroy(gameObject);
        }
    }

}
