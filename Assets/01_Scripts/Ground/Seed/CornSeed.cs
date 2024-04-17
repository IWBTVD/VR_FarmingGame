using UnityEngine;

namespace Jun.Ground.Crops
{
    public class CornSeed : SeedBase
    {
        public float currentXAngle;
        private void Start()
        {
            // seedType = SeedType.Seed;
            StartSeed = true;
        }

        public void Update()
        {
            currentXAngle = transform.rotation.eulerAngles.x;

        }

        //지금 cornseed 는 seedbase를 상속받아, 이때 seedbase의 ontriggerenter를 오버라이드 하려면 어떻게해?

        private void OnTriggerEnter(Collider other)
        {
            if (seedType == SeedType.Crops)
            {
                if (other.CompareTag("CropPoint"))
                {
                    //퀘스트 체크
                    if (QuestManager.instance.currentQuestID == 2)
                        QuestManager.instance.CheckQuestBehavior(GetComponent<Quest>().GetBehaviorID());//퀘스트가 진행 후에도 계속 실행됨 -> 수정 필요

                    //씨앗이 심기는 기능
                    Debug.Log("Plant Crops");
                    other.GetComponent<CropPoint>().PlantCrop(this);
                    Destroy(gameObject);
                }
            }
        }

    }
}
