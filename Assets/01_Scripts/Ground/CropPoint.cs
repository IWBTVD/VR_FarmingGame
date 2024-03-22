using UnityEngine;
using Photon.Pun;
using System.Collections;


namespace Jun.Ground.Crops
{
    public class CropPoint : MonoBehaviourPunCallbacks, IPunObservable
    {
        private GameObject seedlings;
        private RowCropsGround rowCropsGround;
        public bool IsSeedlings = false;

        public void Start()
        {
            seedlings = null;

        }

        public void ReceiveSeed(GameObject IncomeSeed)
        {
            seedlings = IncomeSeed;
            IsSeedlings = true;
            // NotifyCrop();
        }

        public void PlantCrops(GameObject IncomeCrops)
        {
            seedlings = IncomeCrops;
            IsSeedlings = true;
            // NotifyCrop();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}