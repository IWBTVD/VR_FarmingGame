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
                if (other.CompareTag("CropPoint"))
                {
                    Debug.Log("Send Seed");
                    bool IsSeedlingsGround = other.GetComponent<CropPoint>().IsPlanted;

                    if (!IsSeedlingsGround)
                    {
                        other.GetComponent<CropPoint>().ReceiveSeed(this);
                    }

                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (seedType == SeedType.Crops)
            {
                if (other.CompareTag("CropPoint"))
                {
                    Debug.Log("Plant Crops");
                    other.GetComponent<CropPoint>().PlantCrop(this);
                    Destroy(gameObject);
                }
            }
        }


        // public void PlayParticleByAngle(float currentXAngle)
        // {
        //     if ((currentXAngle >= 35f))
        //     {
        //         StartSeed = true;
        //     }
        //     else
        //     {
        //         StartSeed = false;
        //     }
        // }

    }
}