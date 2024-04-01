using UnityEngine;
using Jun.Ground.Crops;
using Photon.Pun;

namespace Jun
{
    public class SeedParticle : MonoBehaviourPun
    {
        private ParticleSystem ps;
        private readonly bool StartSeed = false;
        public GameObject currentCropsPrefab;
        private SeedBase seedBase;

        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
            ps.Play();
            seedBase = GetComponentInParent<SeedBase>();
            currentCropsPrefab = seedBase.currentCropsPrefab;

        }

        private void Update()
        {
            // TestCode();
        }

        public void TestCode()
        {
            if (StartSeed)
            {

                if (!ps.isPlaying)
                {
                    ps.Play();
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


        private void OnParticleCollision(GameObject other)
        {

            if (other.CompareTag("CropPoint"))
            {
                Debug.Log("Send Seed");
                bool IsSeedlingsGround = other.GetComponent<CropsGround>().IsSeedlings;

                if (!IsSeedlingsGround)
                {
                    other.GetComponent<CropPoint>().ReceiveSeed(seedBase);
                }
            }

        }
    }
}
