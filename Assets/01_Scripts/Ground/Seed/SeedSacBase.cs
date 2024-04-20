using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Autohand;

namespace Jun.Ground.Crops
{
    /// <summary>
    /// 씨앗이 들어있는 주머니
    /// </summary>
    public class SeedSacBase : MonoBehaviourPun
    {

        [SerializeField] protected Transform seedSpawnPoint;
        [Space]
        [SerializeField] protected SeedBase seedPrefab;
        protected int seedCapacity = 10;

        protected Grabbable grabbable;
        protected SeedBase currentSeed;

        protected virtual void Awake()
        {
            grabbable ??= GetComponent<Grabbable>();
            grabbable.onSqueeze.AddListener(OnSqueeze);
            grabbable.onUnsqueeze.AddListener(OnUnsqueeze);  
        }

        /// <summary>
        /// 스퀴즈하면 씨앗이 나온다
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="_grabbable"></param>
        public void OnSqueeze(Hand hand, Grabbable grabbable)
        {
            if(seedCapacity > 0)
            {
                currentSeed = Instantiate(seedPrefab, seedSpawnPoint);
                currentSeed.CreatedBySeedSac(this);
                seedCapacity--;
            }
        }

        public void OnUnsqueeze(Hand hand, Grabbable grabbable)
        {
            if(currentSeed != null)
            {
                currentSeed.DecoupleWithSeedSac();
            }
        }
    }
}