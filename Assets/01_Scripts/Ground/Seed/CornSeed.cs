using UnityEngine;

namespace Jun.Ground.Crops
{
    public class CornSeed : SeedBase
    {
        public GameObject prefab;
        private void Start()
        {
            ps = GetComponent<ParticleSystem>();
            seedType = SeedType.Seed;
            StartSeed = true;
        }

        public void Update()
        {
            TestCode();
        }

    }
}
