using UnityEngine;
using Photon.Pun;
using System.Collections;
using Gun;

namespace Jun.Ground.Crops
{
    public class CropPoint : MonoBehaviour
    {
        private const float MAX_CULTIVATION_TIME = 2f;

        private CultivationField _cultivationField;
        public CultivationField CultivationField
        {
            get
            {
                if (_cultivationField == null) _cultivationField = GetComponentInParent<CultivationField>();
                return _cultivationField;
            }
        }
        public bool IsWatered => CultivationField.IsWatered;

        public GameObject seedlings;
        private CropMound rowCropsGround;
        public bool IsSeedlings = false;
        public bool IsCultivation = false;
        public GameObject cultivationObject;
        public ParticleSystem particle;
        private float cultivationTime = 0f;

        public bool IsSeedlings = false;
        public bool IsCultivation = false;
        public bool IsPsPlaying = false;

        public void Start()
        {
            particle = GetComponentInChildren<ParticleSystem>();
            // 파티클 시스템을 비활성화
            seedlings = null;
            rowCropsGround = GetComponentInParent<CropMound>();
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
            particle.Play();
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
            particle.Play();
        }

        private void SeedGrowing()
        {
            if (IsWatered && IsSeedlings)
            {
                cultivationTime += Time.deltaTime;
                if (cultivationTime >= MAX_CULTIVATION_TIME)
                {
                    Instantiate(seedlings, cultivationObject.transform);
                    IsCultivation = false;
                    IsSeedlings = false;
                    cultivationTime = 0f;
                }
            }
            else if (!IsWatered && IsSeedlings)
            {
                cultivationTime += Time.deltaTime / 10;
                if (cultivationTime >= MAX_CULTIVATION_TIME)
                {
                    Instantiate(seedlings, cultivationObject.transform);
                    IsCultivation = false;
                    IsSeedlings = false;
                    cultivationTime = 0f;
                }
            }
        }

        public void Harvest()
        {
            rowCropsGround.NotifyRemoveCrop(this.gameObject, seedlings);
        }

        // EnableChanged 이벤트를 감지하여 파티클 시스템을 제어
        private void OnEnable()
        {
            // enable될 때 파티클을 멈추도록 설정
            particle.Stop();
        }
    }
}
