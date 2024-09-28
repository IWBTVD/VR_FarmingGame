using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WateringCan : MonoBehaviour, IToolBase
{
    [Header("물뿌리개 출수구 방향 시작점과 끝점")]
    public Transform neckStartTransform;
    public Transform neckEndTransform;

    [Header("물뿌리기 파티클")]
    public ParticleSystem wateringParticle;
    public List<AudioClip> wateringSoundClips = new();

    [Space]
    public Vector3 neckDirection;

    private AudioSource audioSource;
    private Autohand.Grabbable grabbable;

    private int _toolID;  // toolID 값을 저장할 필드

    public int toolID
    {
        get => _toolID;
        set => _toolID = value;
    }


    public void DoAction(CultivationField targetField)
    {
        targetField.WaterGround(10);
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        grabbable = GetComponent<Autohand.Grabbable>();
        toolID = 2;
    }

    void Start()
    {
        wateringParticle.Stop();
        audioSource.Stop();
    }

    void Update()
    {
        if (!grabbable.IsHeld()) return;

        neckDirection = (neckStartTransform.position - neckEndTransform.position).normalized;

        if (neckDirection.y > -0.2f)
        {
            Watering(true);
        }
        else
        {
            Watering(false);
        }
    }

    private void Watering(bool isOn)
    {
        if (isOn)
        {
            if (wateringParticle.isPlaying && audioSource.isPlaying) return;

            wateringParticle.Play();
            audioSource.clip = wateringSoundClips[Random.Range(0, wateringSoundClips.Count - 1)];
            audioSource.Play();
        }
        else
        {
            wateringParticle.Stop();
            audioSource.Pause();
        }
    }
}
