using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gun
{
    public class WateringParticle : MonoBehaviour
    {
        CultivationField lastWateredField;

        private ParticleSystem particle;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
            
        }

        private void OnDisable()
        {
            lastWateredField = null;
        }


        private void OnParticleCollision(GameObject other)
        {
            //GetComponent를 너무 많이 호출하는 것을 방지하기 위한 처리
            if (other.tag == "Soil")
            {
                CultivationField field;

                if (lastWateredField != null)
                {
                    if (lastWateredField.gameObject == other)
                    {
                        field = lastWateredField;
                    }
                    else if (other.TryGetComponent(out field))
                    {
                        lastWateredField = field;
                    }

                    field.WaterGround(2);
                    return;
                }

                else if (other.TryGetComponent(out field))
                {
                    lastWateredField = field;

                    field.WaterGround(2);
                    return;
                }
            }
        }
    }
}
