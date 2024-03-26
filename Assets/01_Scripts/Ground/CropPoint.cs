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
        public bool IsSeedlings = false;
        public bool IsCultivation = false;
        public GameObject cultivationObject;
        public ParticleSystem ps;
        private const float MAXCULTOVATIONTIME = 2f;
        private float cultivationTime = 0f;

        public void Start()
        {
            ps = GetComponentInChildren<ParticleSystem>();
            // 파티클 시스템을 비활성화
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
            // 파티클 시스템 활성화
            ps.Play();
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
            ps.Play();
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

        // EnableChanged 이벤트를 감지하여 파티클 시스템을 제어
        private void OnEnable()
        {
            // enable될 때 파티클을 멈추도록 설정
            ps.Stop();
        }
    }
}
