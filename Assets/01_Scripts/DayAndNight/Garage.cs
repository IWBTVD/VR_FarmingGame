using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Garage : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        // "Item" 태그를 가진 물체와 충돌했을 때
        if (other.CompareTag("Item"))
        {
            // 충돌한 물체의 Item 스크립트를 가져옴
            Item item = other.GetComponent<Item>();
            text.text = item.GetType();
            Debug.Log("Outline");
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     // "Item" 태그를 가진 물체와 충돌에서 벗어났을 때
    //     if (other.CompareTag("Item"))
    //     {

    //         Debug.Log("Outline Off");
    //     }
    // }
}
