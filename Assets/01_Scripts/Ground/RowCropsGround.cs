using Photon.Pun;
using UnityEngine;
using Photon;
using Unity.VisualScripting;
using Jun.Ground.Crops;
using System.Collections.Generic;

public class RowCropsGround : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum GroundState { Dry , Wet }

    
    public GroundState currentState = GroundState.Dry;

    /// <summary>
    /// Material[0] = Dry, Material[1] = Wet
    /// </summary>
    public Material[] materials;
    private GameObject[] CropsRows;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    public float DRY_TIME = 300f;
    private float timeElapsed;

    [SerializeField]
    GameObject cultivationObject;
    private bool IsCultivation;
    public bool IsSeedlings = false;
    private const float MAXCULTOVATIONTIME = 5f;
    public float cultivationTime = 0f;

    [SerializeField]
    //추후 씨앗에 따라 다른 작물이 나오게 하기
    private GameObject seedlings;


    int numCollisionEvents = 0;
    

    void Start()
    {
        CropsRows = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            CropsRows[i] = transform.GetChild(i).gameObject;
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
    
    public void ReceiveSeed(GameObject IncomeSeed)
    {
        seedlings = IncomeSeed;
        IsSeedlings = true;
        photonView.RPC("SyncSeedlings", RpcTarget.AllBuffered, IsSeedlings);
    }

    public void PlantCrops(GameObject IncomeCrops)
    {
        seedlings = IncomeCrops;
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
        // if (other.gameObject.tag == "Tool")
        // {
        //     IsCultivation = true;
        //     cultivationObject.SetActive(true);
        //     photonView.RPC("SyncCultivation", RpcTarget.AllBuffered, IsCultivation);
        // }

        TestTriggerByPosition(other);
    }

    private void TestTriggerByPosition(Collider other)
    {
        if (other.gameObject.tag == "Tool")
        {
            Debug.Log("다른 물체가 해당 물체에 닿았습니다.");
            Vector3 objectBounds = transform.GetComponent<Collider>().bounds.size;
            Vector3 objectBottomLeft = transform.position - new Vector3(objectBounds.x / 2, objectBounds.y / 2, 0);

            Vector3 collisionPoint = other.transform.position;

            float sectionWidth = objectBounds.x / 5;
            for (int i = 0; i < 5; i++)
            {
                float sectionStartX = objectBottomLeft.x + (sectionWidth * i);
                float sectionEndX = sectionStartX + sectionWidth;

                if (collisionPoint.x >= sectionStartX && collisionPoint.x <= sectionEndX && collisionPoint.y <= objectBottomLeft.y)
                {
                    Debug.Log("다른 물체가 해당 물체의 왼쪽 아래에 닿았습니다. (Section: " + (i + 1) + ")");
                }
            }
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
                transform.GetComponent<Renderer>().material = materials[1];
                break;
        
        
            case GroundState.Dry:
                transform.GetComponent<Renderer>().material = materials[0];
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