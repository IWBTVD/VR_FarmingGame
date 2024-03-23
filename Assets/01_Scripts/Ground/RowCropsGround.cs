using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;
using System.Collections.Generic;
using System;

namespace Jun.Ground.Crops
{
    public class RowCropsGround : MonoBehaviourPunCallbacks, IPunObservable
    {
        public static Dictionary<string, GameObject> cropPoints = new Dictionary<string, GameObject>();

        public void NotifyAddCrop(GameObject cropPoint, GameObject seedlings)
        {
            cropPoints.Add(cropPoint.name, seedlings);
            Debug.Log("Add Crop" + cropPoints.Keys + cropPoints.Values);
        }

        public void NotifyRemoveCrop(GameObject cropPoint, GameObject seedlings)
        {
            cropPoints.Remove(cropPoint.name);
            Debug.Log("Remove Crop" + cropPoints.Keys + cropPoints.Values);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}