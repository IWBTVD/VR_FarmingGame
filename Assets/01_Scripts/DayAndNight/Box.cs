using TMPro;
using UnityEngine;
using System.Collections.Generic;

namespace Jun
{
    public class Box : MonoBehaviour
    {
        Dictionary<string, int> itemAmountDict = new Dictionary<string, int>();
        Dictionary<string, GameObject> itemTextDict = new Dictionary<string, GameObject>();
        public GameObject TextPrefab;
        public Transform Canvas;


        // 작물등 판매가능한 아이템들을 스크립트로 관리 해야함.
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                string itemName = other.name;

                if (itemAmountDict.ContainsKey(itemName))
                {
                    itemAmountDict[itemName]++;
                    itemTextDict[itemName].GetComponent<TextMeshProUGUI>().text = itemName + " x" + itemAmountDict[itemName];
                }
                else
                {
                    GameObject newText = Instantiate(TextPrefab, Canvas);
                    newText.GetComponent<TextMeshProUGUI>().text = itemName;
                    itemTextDict[itemName] = newText;
                    itemAmountDict[itemName] = 1;
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
    }
}
