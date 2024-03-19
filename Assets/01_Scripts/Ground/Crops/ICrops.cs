using UnityEngine;
using Photon.Pun;

namespace Jun.Ground.Crops
{
    public interface ICrops
    {
        void SendSeed();
        void SeedGrowing();
        void HarvestCrops();
        
    }
}