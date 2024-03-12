using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCtrl : MonoBehaviourPun, IPunObservable
{
    struct SyncData //Vector3 와 쿼터니언값을 갖는 구조체
    {
        public Vector3 pos;
        public Quaternion rot;
    };

    public GameObject[] myModel;
    public GameObject otherModel;

    void Awake()
    {
        if (photonView.IsMine)
        {
            for(int i =0; i < myModel.Length; i++)
            {
                myModel[i].SetActive(true);
            }
        }
        else
        {
            otherModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }
}
