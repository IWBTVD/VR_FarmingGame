using UnityEngine;
using Photon.Pun;
using System.Collections;


namespace Jun.Ground.Crops
{
    public class CropPoint : MonoBehaviourPun, IPunObservable
    {
        public enum GroundState { Dry, Wet }
        public GroundState currentState = GroundState.Dry;
        private GameObject seedlings;
        private RowCropsGround rowCropsGround;
        public bool IsSeedlings = false;
        public bool IsCultivation = false;
        public GameObject cultivationObject;
        private const float MAXCULTOVATIONTIME = 2f;
        private float cultivationTime = 0f;


        public void Start()
        {
            seedlings = null;
            rowCropsGround = GetComponentInParent<RowCropsGround>();
            cultivationObject = transform.gameObject;
        }

        public void Update()
        {
            SeedGrowing();
        }

        public void ReceiveSeed(GameObject IncomeSeed)
        {
            if (IsSeedlings == true)
            {
                return;
            }
            seedlings = IncomeSeed;
            IsSeedlings = true;
            rowCropsGround.NotifyAddCrop(this.gameObject, seedlings);
            // NotifyCrop();
        }

        public void PlantCrops(GameObject IncomeCrops)
        {
            if (IsSeedlings == true)
            {
                return;
            }
            seedlings = IncomeCrops;
            IsSeedlings = true;
            rowCropsGround.NotifyAddCrop(this.gameObject, seedlings);
            // NotifyCrop();
        }

        private void SeedGrowing()
        {
            if (currentState == GroundState.Wet && IsSeedlings)
            {
                cultivationTime += Time.deltaTime;
                if (cultivationTime >= MAXCULTOVATIONTIME)
                {
                    Instantiate(seedlings, cultivationObject.transform);
                    IsCultivation = false;
                    IsSeedlings = false;
                    cultivationTime = 0f;
                }
            }
            else if (currentState == GroundState.Dry && IsSeedlings)
            {
                cultivationTime += Time.deltaTime / 10;
                if (cultivationTime >= MAXCULTOVATIONTIME)
                {
                    Instantiate(seedlings, cultivationObject.transform);
                    IsCultivation = false;
                    IsSeedlings = false;
                    cultivationTime = 0f;
                }
            }
        }

        public void Harvet()
        {
            rowCropsGround.NotifyRemoveCrop(this.gameObject, seedlings);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
}