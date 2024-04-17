using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Autohand;

namespace Jun.Ground.Crops
{
    public class SeedBase : MonoBehaviourPun
    {

        private Grabbable grabbable;


        public enum SeedType
        {
            Seed,
            Crops
        }
        [Header("씨앗 나오는 방향 시작점과 끝점")]
        public Transform neckStartTransform;
        public Transform neckEndTransform;

        [SerializeField] public SeedType seedType;
        [SerializeField] public GameObject currentCropsPrefab;

        [SerializeField] public ParticleSystem ps;

        [Header("Test")]
        public bool StartSeed = false;

        private Vector3 neckDirection;

        private void Awake()
        {
            grabbable ??= GetComponent<Grabbable>();
        }

        private void Update()
        {
            neckDirection = (neckStartTransform.position - neckEndTransform.position).normalized;
        }

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