using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;
using System.Collections.Generic;

namespace Jun.Ground.Crops
{
    public class RowCropsGround : MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] public static CropPoint[] cropPoints;

        public static void NotifyCrop(GameObject seedlings)
        {
            foreach (CropPoint cropPoint in cropPoints)
            {
                cropPoint.GetComponent<CropPoint>().ReceiveSeed(seedlings);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}