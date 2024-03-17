using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Gun.Photon.Player
{
    public class PhotonPlayer : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private Transform headTransform;
        [SerializeField] private Transform leftHandTransform;
        [SerializeField] private Transform rightHandTransform;

        private SyncStruct head;
        private SyncStruct leftHand;
        private SyncStruct rightHand;

        [Space]
        [SerializeField] private Transform avatarHead;
        [SerializeField] private Transform avatarLeftHand;
        [SerializeField] private Transform avatarRightHand;

        void Start()
        {

        }
        void Update()
        {
            if(photonView.IsMine)
            {
                head.position = headTransform.position;
                head.rotation = headTransform.rotation;

                leftHand.position = leftHandTransform.position;
                leftHand.rotation = leftHandTransform.rotation;

                rightHand.position = rightHandTransform.position;
                rightHand.rotation = rightHandTransform.rotation;

                avatarHead.SetPositionAndRotation(head.position, head.rotation);
                avatarLeftHand.SetPositionAndRotation(leftHand.position, leftHand.rotation);
                avatarRightHand.SetPositionAndRotation(rightHand.position, rightHand.rotation);
            }
            else
            {
                avatarHead.SetPositionAndRotation(head.position, head.rotation);
                avatarLeftHand.SetPositionAndRotation(leftHand.position, leftHand.rotation);
                avatarRightHand.SetPositionAndRotation(rightHand.position, rightHand.rotation);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(head);
                stream.SendNext(leftHand);
                stream.SendNext(rightHand);
            }
            else
            {
                head = (SyncStruct)stream.ReceiveNext();
                leftHand = (SyncStruct)stream.ReceiveNext();
                rightHand = (SyncStruct)stream.ReceiveNext();
            }
        }
    }

    [System.Serializable]
    public struct SyncStruct
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}