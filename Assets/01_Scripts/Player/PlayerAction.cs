using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEditor;

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

    [Header("Soils")]
    public LayerMask GroundLayers;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.2f;
    bool isSoil;

    [Space(10)]
    [Header("Obstacles")]
    public LayerMask ObstacleLayers;
    public float ObstacleOffset = -0.14f;
    public float ObstacleRadius;
    bool isObstacle;

    bool tool0;
    bool tool1;
    bool tool2;

    bool isAction;
    public bool isMining;

    bool isTestCode;

    public int currentIndex = -1;

    [SerializeField]
    GameObject nearObject;
    CultivationField nearSoil;
    [SerializeField]
    BreakableObject nearBreakable;

    ThirdPersonController controllerFarmer;

    GameObject equippedTool;


    void Awake()
    {
        controllerFarmer = GetComponent<ThirdPersonController>();
        hasTools = new bool[5];
    }

    void FixedUpdate()
    {
        GetInput();

        Interaction();

        SoilCheck();
        ObstacleCheck();

        DoAction();
        ChangeTool();

        // 테스트용 함수
        // TestCode();
    }

    void TestCode()
    {

    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interaction");
        tool0 = Input.GetButton("Tool0");
        tool1 = Input.GetButton("Tool1");
        tool2 = Input.GetButton("Tool2");
        isAction = Input.GetButton("Fire1");
        isTestCode = Input.GetButton("TestCode");
    }

    void ChangeTool()
    {
        if (tool0 && hasTools[0]) currentIndex = 0;
        if (tool1 && hasTools[1]) currentIndex = 1;
        if (tool2 && hasTools[2]) currentIndex = 2;

        if (currentIndex == -1) return;

        if (equippedTool != null)
            equippedTool.SetActive(false);

        equippedTool = Tools[currentIndex];
        equippedTool.SetActive(true);


    }

    void DoAction()
    {
        if (currentIndex == -1) return;

        switch (currentIndex)
        {
            case 0:
            case 2:
                if (isAction && nearSoil != null)
                {
                    Tools[currentIndex].GetComponent<IToolBase>().DoAction(nearSoil);
                }
                break;
            case 1:
                if (isAction && nearBreakable != null)
                {
                    if (!controllerFarmer._animator.GetBool("IsMining"))
                    {
                        isMining = true;
                        controllerFarmer.isMining = true;
                        controllerFarmer._animator.SetBool("IsMining", isMining);
                        Tools[currentIndex].GetComponent<ICanBreak>().DoAction(nearBreakable);
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 지면에 닿아있는지 확인
    /// GroundLayers에 속하는 콜라이더를 찾고, 태그가 "Soil"인 오브젝트를 찾음.
    /// 만약 찾은 오브젝트가 있으면 isSoil을 true로, nearSoil을 그 오브젝트로 정의.
    /// 만약 찾은 오브젝트가 없으면 isSoil을 false로, nearSoil을 null로 정의.
    /// </summary>
    void SoilCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);

        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        nearSoil = null;

        if (hitColliders.Length > 0)
        {
            isSoil = true;
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Soil"))
                {
                    nearSoil = hitCollider.gameObject.GetComponent<CultivationField>();
                    break;
                }
            }
        }
        else
        {
            isSoil = false;
        }
    }


    /// <summary>
    /// 주위에 있는 부술 수 있는 오브젝트 확인하는 함수.
    /// ObstacleLayers에 속하는 콜라이더를 찾고, 태그가 "Obstacle"인 오브젝트를 찾음.
    /// 찾은 오브젝트가 있으면 isObstacle을 true로, nearBreakable을 그 오브젝트로 정의.
    /// 찾은 오브젝트가 없으면 isObstacle을 false로, nearBreakable을 null로 정의.
    /// </summary>
    void ObstacleCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - ObstacleOffset, transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, ObstacleRadius, ObstacleLayers, QueryTriggerInteraction.Ignore);

        nearBreakable = null;

        if (hitColliders.Length > 0)
        {
            isObstacle = true;
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Obstacle"))
                {
                    nearBreakable = hitCollider.gameObject.GetComponent<BreakableObject>();
                    break;
                }
            }
        }
        else
        {
            isObstacle = false;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - ObstacleOffset, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, ObstacleRadius);

    }
    // VR기기용 코드
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Tool")
        {
            nearObject = col.gameObject;
            Debug.Log("Near Object : " + nearObject.name);
        }

        if (col.tag == "Breaker")
        {
            nearObject = col.gameObject;
            Debug.Log("Near Object : " + nearObject.name);
        }
    }


    // 키보드기기용 코드
    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Tool")
            {

                IToolBase toolBase = nearObject.GetComponent<IToolBase>();
                int toolIndex = toolBase.toolID;

                hasTools[toolIndex] = true;

                Debug.Log("Tool added to slot " + toolIndex + ": " + nearObject.name);
            }

            if (nearObject.tag == "Breaker")
            {
                ICanBreak breaker = nearObject.GetComponent<ICanBreak>();
                int breakerIndex = breaker.GetID();

                hasTools[breakerIndex] = true;
                Debug.Log("Tool added to slot " + breakerIndex + ": " + nearObject.name);
            }
        }
    }

    // 광질 애니메이션이 끝났을 때
    public void EndMining()
    {
        isMining = false;
        controllerFarmer.isMining = isMining;
        controllerFarmer._animator.SetBool("IsMining", isMining);
    }
}
