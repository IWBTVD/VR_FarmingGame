using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun 
{
    /// <summary>
    /// 도구를 사용했을 때 그 자리에서 잠깐 유지되어 파티클이 재생되다가 재생이 끝나면 다시 도구의 자식으로 돌아오는 파티클
    /// </summary>
    public class ToolParticleEffect : MonoBehaviour
    {
        private Transform originalParent;
        private ParticleSystem particle;

        private void Awake()
        {
            originalParent = transform.parent;
            gameObject.SetActive(false);

            particle = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if(!particle.IsAlive())
            {
                transform.SetParent(originalParent);
                transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {

        }

        /// <summary>
        /// 잠시 부모에게서 떨어져나오면서 파티클 재생
        /// </summary>
        /// <returns></returns>
        public bool PlayParticle()
        {
            if (gameObject.activeSelf) return false;

            gameObject.SetActive(true);
            transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            transform.SetParent(null);

            particle.Play();

            return true;
        }
    }
}