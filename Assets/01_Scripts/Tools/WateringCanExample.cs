using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gun
{
    public class WateringCanExample : MonoBehaviour
    {
        [Header("���Ѹ��� ����� ���� �������� ����")]
        public Transform neckStartTransform;
        public Transform neckEndTransform;

        [Header("���Ѹ��� ��ƼŬ")]
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