using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gun
{
    public class WateringCanExample : MonoBehaviour
    {
        [Header("물뿌리개 출수구 방향 시작점과 끝점")]
        public Transform neckStartTransform;
        public Transform neckEndTransform;

        [Header("물뿌리기 파티클")]
        public ParticleSystem wateringParticle;

        [Space]
        public Vector3 neckDirection;

        void Start()
        {
            wateringParticle.Stop();
        }

        void Update()
        {
            neckDirection = (neckStartTransform.position - neckEndTransform.position).normalized;

            if (neckDirection.y > -0.2f)
            {
                wateringParticle.Play();
            }
            else
            {
                wateringParticle.Stop();
            }
        }
    }
}