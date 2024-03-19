using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using ExitGames.Client.Photon.StructWrapping;

namespace Jun.Ground.Crops
{
    public class Seed : MonoBehaviourPunCallbacks, ICrops
    {
        public enum SeedType
        {
            Tomato,
            Potato
        }
        [SerializeField]
        public GameObject TomatoPrefab;
        public GameObject PotatoPrefab;

        public SeedType seedType;
        public bool StartSeed = false;

        private GameObject CurrentCrops;

        private ParticleSystem ps;

        private void Start(){
            ps = GetComponent<ParticleSystem>();

            switch (seedType)
            {
                case SeedType.Tomato:
                    CurrentCrops = TomatoPrefab;
                    break;
                case SeedType.Potato:
                    CurrentCrops = PotatoPrefab;
                    break;
            }
        }

        public void Update()
        {
            if(StartSeed)
            {
                ps.Play();
            }else{
                ps.Stop();
            }
        }

        public void SendSeed()
        {
            throw new System.NotImplementedException();
        }
        public void SeedGrowing()
        {
            throw new System.NotImplementedException();
        }
        public void HarvestCrops()
        {
            throw new System.NotImplementedException();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (other.CompareTag("Ground"))
            {
                Debug.Log("Send Seed");
                bool IsSeedlingsGround = other.GetComponent<CropsGround>().GetIsSeedlings();
                
                if(!IsSeedlingsGround) other.GetComponent<CropsGround>().ReceiveSeed(CurrentCrops);
                else Destroy(gameObject);
            }
        }
    }
}