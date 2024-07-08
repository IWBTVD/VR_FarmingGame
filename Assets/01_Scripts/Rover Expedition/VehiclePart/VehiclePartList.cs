using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehiclePartList", menuName = "ScriptableObject/VehiclePartListSO", order =2)]
public class VehiclePartList : ScriptableObject
{
  
     public List<VehicleStructure> parts;   //부품 정보 리스트
    

    
}
