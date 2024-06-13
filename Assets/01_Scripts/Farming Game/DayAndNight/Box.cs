using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Jun
{
    public class Box : MonoBehaviour
    {
        List<Item> items = new List<Item>();
        Dictionary<string, int> itemAmountDict = new Dictionary<string, int>();
        Dictionary<string, GameObject> itemTextDict = new Dictionary<string, GameObject>();
        public GameObject textPrefab;
        public Transform canvas;


        // 작물등 판매가능한 아이템들을 스크립트로 관리 해야함.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                Item item = other.GetComponent<Item>();
                string itemName = item.itemType.ToString();

                if (itemAmountDict.ContainsKey(itemName))
                {
                    itemAmountDict[itemName]++;
                    itemTextDict[itemName].GetComponent<TextMeshProUGUI>().text = itemName + " x" + itemAmountDict[itemName];
                }
                else
                {
                    GameObject newText = Instantiate(textPrefab, canvas);
                    newText.GetComponent<TextMeshProUGUI>().text = itemName;
                    itemTextDict[itemName] = newText;
                    itemAmountDict[itemName] = 1;
                    items.Add(item);
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                string itemName = other.name;

                if (itemAmountDict.ContainsKey(itemName))
                {
                    itemAmountDict[itemName]--;
                    items.Remove(other.GetComponent<Item>());

                    if (itemAmountDict[itemName] == 0)
                    {
                        Destroy(itemTextDict[itemName]);
                        itemTextDict.Remove(itemName);
                        itemAmountDict.Remove(itemName);
                    }
                    else
                    {
                        itemTextDict[itemName].GetComponent<TextMeshProUGUI>().text = itemName + " x" + itemAmountDict[itemName];
                    }
                }
            }
        }

        public void SellItem()
        {
            int totalMoney = 0;

            foreach (var item in items)
            {
                totalMoney += item.price * itemAmountDict[item.itemType.ToString()];
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
