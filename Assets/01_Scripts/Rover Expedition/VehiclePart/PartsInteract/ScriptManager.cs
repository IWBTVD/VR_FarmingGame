using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public VehicleStructure VSSO;                


    void Start()
    {
        AddScriptBasedOnIdentifier();       
    }

    private void AddScriptBasedOnIdentifier()    //스크립터블 오브젝트의 부품타입(string)을 인식하여 해당하는 스크립트를 오브젝트에 추가
    {
        switch (VSSO.PartType)
        {
            case "CollectionTool":               //수집 도구일 경우
                if (gameObject.GetComponent<CollectionToolScript>() == null)
                {
                    gameObject.AddComponent<CollectionToolScript>();
                }
                break;
            
            default:
                Debug.LogWarning("Unknown Part: " + VSSO.PartType);
                break;
        }
    }
}
