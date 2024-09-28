using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // 0 : Pichfork
    // 1 : Pickaxe
    // 2 : WateringCan
    // 3 : Seed
    // 4 : 미정
    public GameObject[] Tools;
    public bool[] hasTools;

    bool iDown;

    public LayerMask GroundLayers;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.2f;
    bool isSoil;

    bool tool0;
    bool tool1;
    bool tool2;

    bool isAction;

    public int currentIndex = -1;

    GameObject nearObject;
    CultivationField nearSoil;

    void Awake()
    {
        Tools = new GameObject[5];
        hasTools = new bool[5];

    }

    void Update()
    {
        GetInput();
        Interaction();
        SoilCheck();
        DoAction();
        ChangeTool();
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interaction");
        tool0 = Input.GetButton("Tool0");
        tool1 = Input.GetButton("Tool1");
        tool2 = Input.GetButton("Tool2");
        isAction = Input.GetButton("Fire1");
    }

    void ChangeTool()
    {

        if (tool0 && hasTools[0]) currentIndex = 0;
        if (tool1 && hasTools[1]) currentIndex = 1;
        if (tool2 && hasTools[2]) currentIndex = 2;

    }

    void DoAction()
    {
        if (currentIndex == -1) return;

        if (isAction && nearSoil != null)
        {
            Tools[currentIndex].GetComponent<IToolBase>().DoAction(nearSoil);
        }
    }

    void SoilCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);

        // Check if the player is on soil and get the colliders in the radius
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        // Reset the nearSoil variable
        nearSoil = null;

        // Check if we hit any soil objects
        if (hitColliders.Length > 0)
        {
            isSoil = true;
            foreach (Collider hitCollider in hitColliders)
            {
                // Assuming the ground has a "Soil" tag, or you can use layers to differentiate
                if (hitCollider.CompareTag("Soil"))
                {
                    // Store the soil GameObject
                    nearSoil = hitCollider.gameObject.GetComponent<CultivationField>();
                    Debug.Log("Standing on soil: " + nearSoil.name);
                    break; // Stop after finding the first soil
                }
            }
        }
        else
        {
            isSoil = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Tool")
        {
            nearObject = col.gameObject;
            Debug.Log("Near Object : " + nearObject.name);
        }
    }



    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Tool")
            {
                // nearObject에서 IToolBase 컴포넌트를 가져옴
                IToolBase toolBase = nearObject.GetComponent<IToolBase>();
                int toolIndex = toolBase.toolID;

                // hasTools 배열에 도구가 있음을 표시
                hasTools[toolIndex] = true;

                // Tools 배열에 해당 도구를 추가
                Tools[toolIndex] = nearObject;

                // 이제 nearObject에 있는 도구가 Tools 배열에 할당됨
                Debug.Log("Tool added to slot " + toolIndex + ": " + nearObject.name);
            }
        }
    }
}
