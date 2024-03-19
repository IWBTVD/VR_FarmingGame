using UnityEngine;
using Photon.Pun;

namespace Jun.Ground.Crops
{
    public enum CropsState
    {   
        Sprout,
        Growing,
        Harvest
    }
    public interface ICrops
    {
        CropsState CurrentState { get; set; }
        GameObject[] CropsPrefabs { get; set; }
        float CultivationTime { get; set; } 
        float MAXCULTOVATIONTIME { get; set; }


        void ChangePrefab();
        void ChangeState(CropsState newState);
        void CropGrowing();
        void HarvestCrops();
        
    }
}