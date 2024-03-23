using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Jun.Ground.Crops
{
    public class SeedBase : MonoBehaviourPun
    {
        public enum SeedType
        {
            Seed,
            Crops
        }

        [SerializeField] public SeedType seedType;
        [SerializeField] public GameObject currentCropsPrefab;

        [SerializeField] public ParticleSystem ps;

        [Header("Test")]
        public bool StartSeed = false;

        // private void Start()
        // {
        //     switch (seedType)
        //     {
        //         case SeedType.Seed:
        //             ps = GetComponent<ParticleSystem>();
        //             break;
        //         case SeedType.Crops:
        //             break;
        //     }
        // }

        // public void Update()
        // {
        //     TestCode();
        // }

        public void TestCode()
        {
            if (StartSeed)
            {
                if (seedType == SeedType.Seed)
                {
                    if (!ps.isPlaying)
                    {
                        ps.Play();
                    }
                }
            }
            else
            {
                if (seedType == SeedType.Seed)
                {
                    if (ps.isPlaying)
                    {
                        ps.Stop();
                    }
                }
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (seedType == SeedType.Seed)
            {
                if (other.CompareTag("Ground"))
                {
                    Debug.Log("Send Seed");
                    bool IsSeedlingsGround = other.GetComponent<CropsGround>().IsSeedlings;

                    if (!IsSeedlingsGround)
                    {
                        other.GetComponent<CropPoint>().ReceiveSeed(currentCropsPrefab);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (seedType == SeedType.Crops)
            {
                if (other.CompareTag("Ground"))
                {
                    Debug.Log("Plant Crops");
                    other.GetComponent<CropPoint>().PlantCrops(currentCropsPrefab);
                }
            }
        }


        public void PlayParticleByAngle(float currentXAngle)
        {
            if ((currentXAngle >= 35f && currentXAngle <= 100f))
            {
                StartSeed = true;
            }
            else
            {
                StartSeed = false;
            }
        }

    }
}