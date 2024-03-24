using UnityEngine;

namespace Jun.Ground.Crops
{
    public class TomatoSeed : SeedBase
    {
        public float currentXAngle;
        private void Start()
        {
            // seedType = SeedType.Seed;
            StartSeed = true;
        }

        public void Update()
        {
            currentXAngle = transform.rotation.eulerAngles.x;
            // PlayParticleByAngle(currentXAngle);
            TestCode();
        }

    }
}
