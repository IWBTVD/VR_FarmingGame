using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public static SFX_Manager Instance { get; private set; }

    [SerializeField] private List<AudioClip> miningSfxList = new();
    [SerializeField] private List<AudioClip> plowingSfxList = new();

    private void Awake()
    {
        Instance = this;
    }

    public static AudioClip GetPlowingSound()
    {
        return Instance.plowingSfxList[Random.Range(0, Instance.plowingSfxList.Count)];
    }

    public static AudioClip GetMiningSound()
    {
        return Instance.miningSfxList[Random.Range(0, Instance.miningSfxList.Count)];
    }
}
