using Jun;
using Jun.Ground.Crops;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 식물 베이스
/// </summary>
public class PlantBase : MonoBehaviour
{
    #region 변수들
    [SerializeField] protected PlantSO _plantSO;
    [SerializeField] protected List<GameObject> sproutVisualList;
    [SerializeField] protected GameObject matureVisual;

    protected CropPoint _cropPoint;
    [SerializeField]
    protected CultivationField _cultivationField;
    protected int _dayPassed = 0;
    protected int _growthDays = 0;
    #endregion

    public PlantSO PlantSO => _plantSO;
    public CropPoint CropPoint => _cropPoint;
    public CultivationField CultivationField
    {
        get
        {
            return _cultivationField;
        }
    }
    public bool IsWatered => CultivationField.IsWatered;
    /// <summary>
    /// 작물이 심어지고 난 뒤 경과한 일 수
    /// </summary>
    public int DayPassed => _dayPassed;
    /// <summary>
    /// 작물이 성장한 일 수
    /// </summary>
    public int GrowthDays => _growthDays;

    protected virtual void Start()
    {
        OnPlanted();
    }

    void OnEnable()
    {
        _cultivationField = GetComponentInParent<CultivationField>();
        DayNightCycle.Instance.OnDayEnd += OnDayPassed;
    }

    void OnDisable()
    {
        DayNightCycle.Instance.OnDayEnd -= OnDayPassed;
    }

    private void OnDayPassed(object sender, EventArgs e)
    {
        _dayPassed += 1;

        if (IsWatered) _growthDays += 1;
    }


    void Update()
    {
        if (_dayPassed > 2)
        {
            sproutVisualList[1].SetActive(false);

            matureVisual.SetActive(true);
        }
        else if (_dayPassed > 1)
        {
            sproutVisualList[0].SetActive(false);
            sproutVisualList[1].SetActive(true);
        }

    }
    /// <summary>
    /// 막 심어졌을 때 메소드
    /// </summary>
    public virtual void OnPlanted()
    {
        foreach (var sprout in sproutVisualList)
        {
            sprout.gameObject.SetActive(false);
        }
        matureVisual.SetActive(false);
        sproutVisualList[0].SetActive(true);

    }

    /// <summary>
    /// 하루가 경과했을 때 호출되는 메소드
    /// </summary>
    /// <param name="day">기본값은 1이고, 혹여나 하루 이상이 흘러갔을 경우를 위하여 만들어놓음(쓸일 없을 듯)</param>
    public virtual void OnDayPassed(int day = 1)
    {
        _dayPassed += day;

        if (IsWatered) _growthDays += 1;
    }
}

