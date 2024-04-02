using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 파괴 가능한 오브젝트의 도구 마다 받는 데미지 증감율을 표기한 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "Breakable SO", menuName = "농장게임 ScriptableObject/파괴가능한 오브젝트 SO 생성")]
public class BreakableSO : ScriptableObject
{
    [SerializeField] private float pickaxeMultiplier;
    [SerializeField] private float axeMultiplier;
    [SerializeField] private float shovelMultiplier;
    [SerializeField] private float otherMultiplier;

    public float PickaxeDamageMultiplier => pickaxeMultiplier;
    public float AxeDamageMultiplier => axeMultiplier;
    public float ShovelDamageMultiplier => shovelMultiplier;
    public float OtherDamageMultiplier => otherMultiplier;

}
