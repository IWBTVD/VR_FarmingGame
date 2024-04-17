using Jun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class PitchFork : MonoBehaviour
    {
        [SerializeField] private List<ToolParticleEffect> plowParticleList;
        [SerializeField] private List<AudioClip> soundList;

        private Autohand.Grabbable grabbable;

        private Vector3 lastClosestPoint;
        private float forkedDistance;
        private CultivationField lastField;

        private void Awake()
        {
            grabbable = GetComponent<Autohand.Grabbable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Soil")
            {
                if (other.TryGetComponent(out CultivationField cultivationField))
                {
                    lastClosestPoint = other.ClosestPoint(transform.position);
                    PlayPlowParticle();
                    cultivationField.PlowGround(10);

                    lastField = cultivationField;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Soil")
            {
                if (lastField == null)
                {
                    lastField = other.GetComponent<CultivationField>();

                    if (lastField == null) return;
                }
                if (lastField.gameObject != other.gameObject)
                {
                    lastField = other.GetComponent<CultivationField>();
                    return;
                }

                Vector3 closestPoint = other.ClosestPoint(transform.position);

                forkedDistance += Vector3.Distance(closestPoint, lastClosestPoint) * 10f;
                lastClosestPoint = other.ClosestPoint(transform.position);

                if (forkedDistance > 1)
                {
                    //퀘스트 체크
                    if (QuestManager.instance.currentQuestID == 1)
                        QuestManager.instance.CheckQuestBehavior(GetComponent<Quest>().GetBehaviorID());//퀘스트가 진행 후에도 계속 실행됨 -> 수정 필요

                    PlayPlowParticle();
                    lastField.PlowGround((int)forkedDistance);//땅이 경작 되는 함수
                    forkedDistance = 0f;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            lastField = null;
            forkedDistance = 0f;
        }

        public void PlayPlowParticle()
        {
            foreach (var particle in plowParticleList)
            {
                if (particle.PlayParticle())
                {
                    return;
                }
            }
        }
    }
}
