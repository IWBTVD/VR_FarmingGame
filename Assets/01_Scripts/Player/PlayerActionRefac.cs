using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEditor;
using EPOOutline;
using Jun;

public class PlayerActionRefac : MonoBehaviour
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
    bool tool3;

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
        ChangeTool();

        DoAction();

        SoilCheck();
        ObstacleCheck();

        Interaction();
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interaction");
        tool0 = Input.GetButton("Tool0");
        tool1 = Input.GetButton("Tool1");
        tool2 = Input.GetButton("Tool2");
        tool3 = Input.GetButton("Tool3");

        isAction = Input.GetButton("Fire1");
        isTestCode = Input.GetButton("TestCode");
    }

    void ChangeTool()
    {
        if (tool0 && hasTools[0]) currentIndex = 0;
        if (tool1 && hasTools[1]) currentIndex = 1;
        if (tool2 && hasTools[2]) currentIndex = 2;
        if (tool3 && hasTools[3]) currentIndex = 3;

        if (currentIndex == -1) return;

        if (equippedTool != null)
            equippedTool.SetActive(false);

        equippedTool = Tools[currentIndex];
        equippedTool.SetActive(true);


    }

    void DoAction()
    {
        if (currentIndex == -1 || !isAction) return;

        // 필드나 부술 수 있는 오브젝트 설정
        switch (currentIndex)
        {
            case 0: // Pichfork or WateringCan
            case 2:
                if (nearSoil != null)
                {
                    Tools[currentIndex].GetComponent<IFiledBase>().SetField(nearSoil);
                }
                break;
            case 1: // Pickaxe
                if (nearBreakable != null && !controllerFarmer._animator.GetBool("IsMining"))
                {
                    isMining = true;
                    controllerFarmer.isMining = true;
                    controllerFarmer._animator.SetBool("IsMining", isMining);
                    Tools[currentIndex].GetComponent<ICanBreak>().SetBreakableObject(nearBreakable);
                }
                break;
            case 3: // Seed

                break;
            default:
                return;
        }

        Tools[currentIndex].GetComponent<IToolBase>().DoAction();
    }


    void CheckForObject<T>(float offset, float radius, LayerMask layers, string targetTag, ref bool isObject, ref T nearObject, Action<T> onObjectFound = null) where T : MonoBehaviour
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - offset, transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(spherePosition, radius, layers, QueryTriggerInteraction.Ignore);

        isObject = false;
        nearObject = null;

        if (hitColliders.Length > 0)
        {
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag(targetTag))
                {
                    nearObject = hitCollider.GetComponent<T>();
                    if (nearObject != null)
                    {
                        isObject = true;
                        onObjectFound?.Invoke(nearObject);
                    }
                    break;
                }
            }
        }
    }

    void SoilCheck()
    {
        CheckForObject(
            GroundedOffset,
            GroundedRadius,
            GroundLayers,
            "Soil",
            ref isSoil,
            ref nearSoil
        );
    }

    void ObstacleCheck()
    {
        if (currentIndex != 1) return;

        CheckForObject(
            ObstacleOffset,
            ObstacleRadius,
            ObstacleLayers,
            "Obstacle",
            ref isObstacle,
            ref nearBreakable,
            nearBreakable =>
            {
                nearBreakable._outlinable.OutlineParameters.Enabled = true;
            }
        );

        if (nearBreakable != null)
        {
            nearBreakable._outlinable.OutlineParameters.Enabled = true;
        }
    }


    void OnDrawGizmos()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - ObstacleOffset, transform.position.z);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePosition, ObstacleRadius);

    }

    void HandleOutline(Collider col, bool enabled)
    {
        if (col.TryGetComponent(out Outlinable _outlinable))
        {
            _outlinable.OutlineParameters.Enabled = enabled;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        // Tool 또는 Seed 태그만 인식
        if (col.CompareTag("Tool") || col.CompareTag("Breaker") || col.CompareTag("Seed"))
        {
            HandleOutline(col, true);
            nearObject = col.gameObject;
            Debug.Log("Near Object : " + nearObject.name);
        }
    }


    void OnTriggerExit(Collider col)
    {
        HandleOutline(col, false);
        nearObject = null;
    }



    // 키보드기기용 코드
    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            HandleToolPickup(nearObject);
        }
    }

    void HandleToolPickup(GameObject obj)
    {
        string objTag = obj.tag;
        IToolBase toolBase = obj.GetComponent<IToolBase>();

        if (toolBase != null)
        {
            int toolIndex = toolBase.toolID;

            if (objTag == "Tool" || objTag == "Breaker" || objTag == "Seed")
            {
                hasTools[toolIndex] = true;
                Debug.Log($"{objTag} added to slot {toolIndex}: {obj.name}");
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
