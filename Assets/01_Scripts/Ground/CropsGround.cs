using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;

public class CropsGround : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum GroundState { Dry , Wet }

    
    public GroundState currentState = GroundState.Dry;

    //Material[0] = Dry, Material[1] = Wet
    public Material[] materials;
    private GameObject[] childObjects;
    private ParticleCollisionEvent[] collisionEvents;

    public float DRY_TIME = 3f;
    private float timeElapsed;

    [SerializeField]
    GameObject cultivationObject;
    private bool IsCultivation;
    private bool IsSeedlings;
    private const float MAXCULTOVATIONTIME = 5f;
    public float cultivationTime = 0f;

    [SerializeField]
    //추후 씨앗에 따라 다른 작물이 나오게 하기
    private GameObject seedlings;


    int numCollisionEvents = 0;
    

    void Start()
    {
        childObjects = new GameObject[2];
        collisionEvents = new ParticleCollisionEvent[16]; 

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        DryGround();

        SeedGrowing();

        TestCode();
    }

    private void DryGround()
    {
        if (currentState == GroundState.Wet && timeElapsed < DRY_TIME)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= DRY_TIME)
            {
                SetState(GroundState.Dry);
                UpdateMaterial();
                timeElapsed = 0f;
                numCollisionEvents = 0;
                photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
            }
        }
    }
    public bool GetIsSeedlings()
    {
        return IsSeedlings;
    }   
    public void ReceiveSeed(GameObject IncomeSeed)
    {
        seedlings = IncomeSeed;
        IsSeedlings = true;
        photonView.RPC("SyncSeedlings", RpcTarget.AllBuffered, IsSeedlings);
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
            photonView.RPC("SyncCultivation", RpcTarget.AllBuffered, IsCultivation);
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

            if(currentState == GroundState.Wet)
            {
                timeElapsed = 0f;
            }

            if (numCollisionEvents >= 5 && currentState == GroundState.Dry)
            {
                SetState(GroundState.Wet);
                UpdateMaterial();
                photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
            }
        }

        // if(other.GetComponent<ParticleSystem>() != null && other.tag == "Seed" && IsCultivation)
        // {
        //     // 씨앗 정보에 따라 다른 작물이 나오게 하기
        //     Seed seed = other.GetComponent<Seed>();
        //     numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);
        //     if (numCollisionEvents >= 5 && currentState == GroundState.Wet)
        //     {
        //         IsSeedlings = true;
        //         photonView.RPC("SyncSeedlings", RpcTarget.AllBuffered, IsSeedlings);
        //     }

        // }
    }

    [PunRPC]
    void SyncState(int state)
    {
        currentState = (GroundState)state;
        UpdateMaterial();
    }
    [PunRPC]
    void SyncCultivation(bool state)
    {
        IsCultivation = state;
    }
    [PunRPC]
    void SyncSeedlings(bool state)
    {
        IsSeedlings = state;
    }


    [PunRPC]
    private void UpdateMaterial()
    {
        switch (currentState)
        {
            case GroundState.Wet:
                for (int i = 0; i < childObjects.Length; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[1];
                }
                for(int i = 0; i < cultivationObject.transform.childCount -1 ; i++)
                {
                    cultivationObject.transform.GetChild(i).GetComponent<Renderer>().material = materials[1];
                    Debug.Log(cultivationObject.transform.GetChild(i).name);
                }
                break;
        
            case GroundState.Dry:
                for (int i = 0; i < childObjects.Length ; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[0];
                }
                for(int i = 0; i < cultivationObject.transform.childCount -1 ; i++)
                {
                    cultivationObject.transform.GetChild(i).GetComponent<Renderer>().material = materials[0];
                    Debug.Log(cultivationObject.transform.GetChild(i).name);
                }
                break;
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
            UpdateMaterial();
        }
    }

    private void TestCode()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetState(GroundState.Wet);
            UpdateMaterial();
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetState(GroundState.Dry);
            UpdateMaterial();
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            //cultivationObject.SetActive(true) 이면 다시 누르면 false로 바꿔주기;
            if(cultivationObject.activeSelf == true)
            {
                cultivationObject.SetActive(false);
            }
            else
            {
                cultivationObject.SetActive(true);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            IsSeedlings = true;
        }
    }
}