using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;   
using Photon.Realtime; 

public class NetworkPlayer : MonoBehaviourPunCallbacks
{
    public GameObject mainCamera;

    private void Start()
    {
        if(photonView.IsMine)
        {
            mainCamera.gameObject.SetActive(true);
        }else{
            mainCamera.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
       if(photonView.IsMine)
        {
            //Do something
            if(Input.GetKeyDown(KeyCode.W))
            {
                transform.position += Vector3.forward;
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                transform.position += Vector3.back;
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                transform.position += Vector3.left;
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                transform.position += Vector3.right;
            }
        }
    }

}
