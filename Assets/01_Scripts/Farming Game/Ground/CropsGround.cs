using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;

public class CropsGround : MonoBehaviourPun, IPunObservable
{
    // 안쓸 코드
    public enum GroundState { Dry, Wet }


    public GroundState currentState = GroundState.Dry;

    //Material[0] = Dry, Material[1] = Wet
    public Material[] materials;
    [SerializeField] public GameObject[] childObjects;
    private ParticleCollisionEvent[] collisionEvents;

    public float DRY_TIME = 3f;
    private float timeElapsed;

    [SerializeField]
    GameObject cultivationObject;
    private bool IsCultivation = true;
    public bool IsSeedlings = false;
    private const float MAXCULTOVATIONTIME = 2f;
    public float cultivationTime = 0f;

    [SerializeField]
    //추후 씨앗에 따라 다른 작물이 나오게 하기
    private GameObject seedlings;


    int numCollisionEvents = 0;


    void Start()
    {
        childObjects = new GameObject[2];
        collisionEvents = new ParticleCollisionEvent[16];

    }

    // public bool GetIsSeedlings()
    // {
    //     return IsSeedlings;
    // }   

    void Update()
    {
        DryGround();

        SeedGrowing();

        // TestCode();
    }

    private void DryGround()
    {
        if (currentState == GroundState.Wet && timeElapsed < DRY_TIME)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= DRY_TIME)
            {
                SetState(GroundState.Dry);
                timeElapsed = 0f;
                numCollisionEvents = 0;
                childObjects[0].GetComponent<MeshRenderer>().material = materials[0];
                childObjects[1].GetComponent<MeshRenderer>().material = materials[0];

            }
        }
    }

    public void ReceiveSeed(GameObject IncomeSeed)
    {
        seedlings = IncomeSeed;
        IsSeedlings = true;

    }

    public void PlantCrops(GameObject IncomeCrops)
    {
        seedlings = IncomeCrops;
        IsSeedlings = true;

    }

    private void SeedGrowing()
    {
        if (currentState == GroundState.Wet && IsSeedlings)
        {
            cultivationTime += Time.deltaTime;
            if (cultivationTime >= MAXCULTOVATIONTIME)
            {
                Instantiate(seedlings, cultivationObject.transform);
                IsCultivation = false;
                IsSeedlings = false;
                cultivationTime = 0f;
            }
        }
        else if (currentState == GroundState.Dry && IsSeedlings)
        {
            cultivationTime += Time.deltaTime / 10;
            if (cultivationTime >= MAXCULTOVATIONTIME)
            {
                Instantiate(seedlings, cultivationObject.transform);
                IsCultivation = false;
                IsSeedlings = false;
                cultivationTime = 0f;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            IsCultivation = true;
            cultivationObject.SetActive(true);

        }
    }

    void SetState(GroundState newState)
    {
        currentState = newState;
    }

    private void OnParticleCollision(GameObject other)
    {

        if (other.GetComponent<ParticleSystem>() != null && other.tag == "Water")
        {

            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);

            if (currentState == GroundState.Wet)
            {
                timeElapsed = 0f;
                childObjects[0].GetComponent<MeshRenderer>().material = materials[1];
                childObjects[1].GetComponent<MeshRenderer>().material = materials[1];
            }

            if (numCollisionEvents >= 5 && currentState == GroundState.Dry)
            {
                SetState(GroundState.Wet);

            }
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext((int)currentState);
            stream.SendNext((bool)IsCultivation);
            stream.SendNext((bool)IsSeedlings);
        }
        else
        {
            currentState = (GroundState)stream.ReceiveNext();
            IsCultivation = (bool)stream.ReceiveNext();
            IsSeedlings = (bool)stream.ReceiveNext();
        }
    }

    private void TestCode()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetState(GroundState.Wet);
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetState(GroundState.Dry);
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //cultivationObject.SetActive(true) 이면 다시 누르면 false로 바꿔주기;
            if (cultivationObject.activeSelf == true)
            {
                cultivationObject.SetActive(false);
            }
            else
            {
                cultivationObject.SetActive(true);
            }

        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            IsSeedlings = true;
        }
    }
}