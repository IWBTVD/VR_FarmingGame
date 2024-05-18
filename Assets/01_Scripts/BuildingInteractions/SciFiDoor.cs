using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoor : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isOpened", true);
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isOpened", true);
    }
}
