using UnityEngine;
using Photon.Pun;
using System.Collections;

namespace Jun.Ground.Crops
{
    public class CropPoint : MonoBehaviourPun, IPunObservable
    {
        public enum GroundState { Dry, Wet }
        public GroundState currentState = GroundState.Dry;
        public GameObject seedlings;
        private RowCropsGround rowCropsGround;
        public GameObject cultivationObject;
        public ParticleSystem ps;


        private const float MAXCULTOVATIONTIME = 2f;
        private float cultivationTime = 0f;

        public bool IsSeedlings = false;
        public bool IsCultivation = false;
        public bool IsPsPlaying = false;

        public void Start()
        {
            ps = GetComponentInChildren<ParticleSystem>();
            seedlings = null;
            rowCropsGround = GetComponentInParent<RowCropsGround>();
            cultivationObject = transform.gameObject;
        }

        public void Update()
        {
            SeedGrowing();

            if (IsPsPlaying)
            {
                if (!ps.isPlaying)
                {
                    ps.Play();
                    IsPsPlaying = false;
                }
            }
            else
            {
                if (ps.isPlaying)
                {
                    ps.Stop();
                }
            }
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
            // 파티클 시스템 활성화
            IsPsPlaying = true;
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
            // 파티클 시스템 활성화
            IsPsPlaying = true;
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
