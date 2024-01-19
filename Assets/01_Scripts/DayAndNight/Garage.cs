// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using EPOOutline;

// public class Garage : MonoBehaviour
// {
//     private Outlinable outlinable;

//     void Start()
//     {
//         outlinable = GetComponent<Outlinable>();

//         // Outline 기본 상태를 비활성화
//         outlinable.enabled = false;
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         // "Item" 태그를 가진 물체와 충돌했을 때
//         if (other.CompareTag("Item"))
//         {
//             // Outline 활성화
//             outlinable.enabled = true;
//             Debug.Log("Outline");
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         // "Item" 태그를 가진 물체와 충돌에서 벗어났을 때
//         if (other.CompareTag("Item"))
//         {
//             // Outline 비활성화
//             outlinable.enabled = false;
//             Debug.Log("Outline Off");
//         }
//     }
// }
