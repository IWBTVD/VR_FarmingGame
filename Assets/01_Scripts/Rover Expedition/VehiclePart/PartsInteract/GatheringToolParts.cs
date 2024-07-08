using Autohand.Demo;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GatheringToolParts : MonoBehaviour
{ 

    public OVRInput.Controller controller;                
    public float InteractionDistance = 10f;             //컨트롤러가 수집물을 인식하는 최대 범위
    public LayerMask InteractableLayer;
    public bool isCollectionEquipmentOn = false;      //수집 장비가 착용되어 있는지


    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller)) //컨트롤러 버튼 누를 때
        {
            CheckForInteraction();
        }
    }

    
    public void InteractWthGleanings()                   //수집물과 상호작용
    {

    }


    public void DisplayInteractBT()                      //수집 버튼 표시
    {

    }


    private void CheckForInteraction()                   //상호작용 가능 여부 확인
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractionDistance, InteractableLayer))
        {
            Gleanings interactable = hit.collider.GetComponent<Gleanings>();
            if (interactable != null)
            {
                interactable.OnInteract();
            }
        }
    }


    public void SetCollectionEquipmentOn(bool On)           //수집 부품을 장착
    {
        isCollectionEquipmentOn = On;
    }

}
