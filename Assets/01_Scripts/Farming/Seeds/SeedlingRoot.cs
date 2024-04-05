using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jun.Ground.Crops;

namespace Gun
{
    /// <summary>
    /// 모종의 뿌리 부분. 이 뿌리가 <see cref="CropPoint"/>에 닿은 상태로 손에서 놓으면 작물이 심어진다
    /// </summary>
    public class SeedlingRoot : MonoBehaviour
    {
        private SeedlingBase seedlingBase;

        private void Awake()
        {
            seedlingBase = GetComponentInParent<SeedlingBase>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "CropPoint")
            {
                if(other.TryGetComponent(out CropPoint cropPoint))
                {
                    seedlingBase.RootPlanted(cropPoint);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "CropPoint")
            {
                if(other.TryGetComponent(out CropPoint cropPoint))
                {
                    seedlingBase.RootUnplanted(cropPoint);
                }
            }
        }
    }
}

