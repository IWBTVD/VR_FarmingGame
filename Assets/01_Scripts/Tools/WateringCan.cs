using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gun
{
    public class WateringCan : MonoBehaviour
    {
        [Header("물뿌리개 출수구 방향 시작점과 끝점")]
        public Transform neckStartTransform;
        public Transform neckEndTransform;

        [Header("물뿌리기 파티클")]
        public ParticleSystem wateringParticle;
        public List<AudioClip> wateringSoundClips = new();

        [Space]
        public Vector3 neckDirection;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            wateringParticle.Stop();
            audioSource.Stop();
        }

        void Update()
        {
            neckDirection = (neckStartTransform.position - neckEndTransform.position).normalized;

            if (neckDirection.y > -0.2f)
            {
                Watering(true);
            }
            else
            {
                Watering(false);
            }
        }

        private void Watering(bool isOn)
        {
            
            if (isOn)
            {
                wateringParticle.Play();
                audioSource.clip = wateringSoundClips[Random.Range(0, wateringSoundClips.Count -1)];
                audioSource.Play();
            }
            else
            {
                wateringParticle.Stop();
                audioSource.Stop();
            }
        }
    }
}