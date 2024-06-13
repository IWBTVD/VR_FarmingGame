using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using static Jun.Item;

namespace Jun
{
    public class BoxNew : MonoBehaviour
    {
        struct ItemInfo
        {
            public string itemName;
            public int itemAmount;
            public GameObject itemText;
            public Item item;
            public int price;

            public ItemInfo(string itemName, int itemAmount, GameObject itemText, Item item, int price)
            {
                this.itemName = itemName;
                this.itemAmount = itemAmount;
                this.itemText = itemText;
                this.item = item;
                this.price = price;
            }

            public void SetItemAmount(int amount)
            {
                itemAmount = amount;
            }
        }

        List<ItemInfo> items = new List<ItemInfo>();

        public GameObject textPrefab;
        public Transform canvas;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                Item otherItem = other.GetComponent<Item>();
                string itemName = otherItem.itemType.ToString();

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemName == itemName)
                    {
                        items[i].SetItemAmount(items[i].itemAmount + 1);
                        items[i].itemText.GetComponent<TextMeshProUGUI>().text = items[i].itemName + " : " + items[i].itemAmount;
                        return;
                    }
                }

                GameObject itemText = Instantiate(textPrefab, canvas);
                itemText.GetComponent<TextMeshProUGUI>().text = itemName + " : 1";

                ItemInfo itemInfo = new ItemInfo(itemName, 1, itemText, otherItem, otherItem.price);
                items.Add(itemInfo);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                Item otherItem = other.GetComponent<Item>();
                string itemName = otherItem.itemType.ToString();

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemName == itemName)
                    {
                        items[i].SetItemAmount(items[i].itemAmount - 1);
                        items[i].itemText.GetComponent<TextMeshProUGUI>().text = items[i].itemName + " : " + items[i].itemAmount;
                        if (items[i].itemAmount == 0)
                        {
                            Destroy(items[i].itemText);
                            items.RemoveAt(i);
                        }
                    }
                }
            }
        }

        public void SellItem()
        {
            int totalMoney = 0;

            //모든 itemAmountDict의 value값을 가져와서 price * itemAmount를 totalMoney에 더해준다.
            foreach (var item in items)
            {
                totalMoney += item.price * item.itemAmount;
            }

            Debug.Log("Total Money: " + totalMoney);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SellItem();
            }
        }
    }
}
