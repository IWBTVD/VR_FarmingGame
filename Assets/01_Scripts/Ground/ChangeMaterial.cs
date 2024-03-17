using Photon.Pun;
using UnityEngine;
using Photon;

public class ChangeMaterial : MonoBehaviourPunCallbacks, IPunObservable
{
    public enum GroundState { Dry , Wet }

    public GroundState currentState = GroundState.Dry;

    //Material[0] = Dry, Material[1] = Wet
    public Material[] materials;
    private GameObject[] childObjects;
    private ParticleCollisionEvent[] collisionEvents;

    private float DRY_TIME = 3f; // ???? ????
    public float timeElapsed; // ???? ????

    int numCollisionEvents = 0;

    void Start()
    {
        childObjects = new GameObject[transform.childCount];
        collisionEvents = new ParticleCollisionEvent[16]; 

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (currentState == GroundState.Wet && timeElapsed < DRY_TIME)
        {
            timeElapsed += Time.deltaTime;

            // ???? ?????? ??????
            if (timeElapsed >= DRY_TIME)
            {
                SetState(GroundState.Dry);
                UpdateMaterial();
                timeElapsed = 0f;
                numCollisionEvents = 0;
                photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SetState(GroundState.Wet);
            UpdateMaterial();
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            SetState(GroundState.Dry);
            UpdateMaterial();
            photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
        }
    }

    void SetState(GroundState newState)
    {
        currentState = newState;
    }

    private void OnParticleCollision(GameObject other)
    {
        
        if (other.GetComponent<ParticleSystem>() != null)
        {
            
            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);

            
            if (numCollisionEvents >= 5 && currentState == GroundState.Dry) // ???? ???????? 5?? ???????? ???? ???? ???? ?????? ??
            {
                SetState(GroundState.Wet);
                UpdateMaterial();
                photonView.RPC("SyncState", RpcTarget.AllBuffered, (int)currentState);
            }
        }
    }

    [PunRPC]
    void SyncState(int state)
    {
        currentState = (GroundState)state;
        UpdateMaterial();
    }


    [PunRPC]
    private void UpdateMaterial()
    {
        switch (currentState)
        {
            case GroundState.Wet:
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[1]; // ???? ?????????? ????
                }
                break;
        
            case GroundState.Dry:
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[0]; // ???? ?????????? ????
                }
                break;
         }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext((int)currentState);
        }
        else
        {
            currentState = (GroundState)stream.ReceiveNext();
            UpdateMaterial();
        }
    }
}