using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehiclePartSO",menuName = "ScriptableObject/VehiclePartSO", order = 1)] //스크립터블 메뉴 생성


public class VehicleStructure : ScriptableObject
{
    public GameObject prefab;   //생성할 프리팹
    public int PartHP;          //부품 체력
    public int PartCost;        //부품 가격
    public int PartWeight;      //부품 무게
    public string PartName;     //부품 이름
    public string PartType;     //부품 분류



    
}
