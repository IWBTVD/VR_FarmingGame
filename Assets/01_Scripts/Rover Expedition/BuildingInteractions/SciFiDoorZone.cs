using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SciFiDoorZone : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("isOpened", true);
    }

    private void OnTriggerStay(Collider other)
    {
        anim.SetBool("isOpened", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("isOpened", false);
    }
}
