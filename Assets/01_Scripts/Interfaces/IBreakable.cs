using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBreakable
{
    /// <summary>
    /// 근접 무기로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithMeleeWeapon(int damage);
    /// <summary>
    /// 원거리 무기로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithRangeWeapon(int damage);
    /// <summary>
    /// 곡괭이로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithPickaxe(int damage);
    /// <summary>
    /// 도끼로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithAxe(int damage);
    /// <summary>
    /// 삽으로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithShovel(int damage);
    /// <summary>
    /// 기타 무기로 공격받았을 때
    /// </summary>
    /// <param name="damage"></param>
    public void OnBreakWithOthers(int damage);
    public void CheckBreak();
}