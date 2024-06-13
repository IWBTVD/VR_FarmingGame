using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jun
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private int Money;

        public int GetMoney()
        {
            return Money;
        }

        public void SetMoney(int money)
        {
            Money = money;
        }

        public void AddMoney(int money)
        {
            Money += money;
        }

        public void SubtractMoney(int money)
        {
            Money -= money;
        }
    }

}
