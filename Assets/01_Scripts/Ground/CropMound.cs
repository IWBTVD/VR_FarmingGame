using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;
using System.Collections.Generic;
using System;

namespace Jun.Ground.Crops
{
    public class CropMound : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] private List<CropPoint> _cropPointList = new();
        public List<CropPoint> CropPointList => _cropPointList;

        public static Dictionary<string, GameObject> cropPoints = new Dictionary<string, GameObject>();

        public void NotifyAddCrop(GameObject cropPoint, GameObject seedlings)
        {
            if (cropPoints.ContainsKey(cropPoint.name))
            {
                return;
            }
            cropPoints.Add(cropPoint.name, seedlings);
            Debug.Log("Add Crop" + cropPoints.Keys + cropPoints.Values);
        }

        public void NotifyRemoveCrop(GameObject cropPoint, GameObject seedlings)
        {
            if (cropPoints.ContainsKey(cropPoint.name))
            {
                return;
            }
            cropPoints.Remove(cropPoint.name);
            Debug.Log("Remove Crop" + cropPoints.Keys + cropPoints.Values);
        }


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}