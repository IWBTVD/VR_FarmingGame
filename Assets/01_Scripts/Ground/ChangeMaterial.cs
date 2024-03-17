using Photon.Pun;
using UnityEngine;
using Photon;

public class ChangeMaterial : MonoBehaviourPunCallbacks, IPunObservable
{
    public Material[] materials;
    private GameObject[] childObjects;
    private ParticleCollisionEvent[] collisionEvents; // 충돌 이벤트 배열

    public bool isWet;
    private float DRY_TIME = 3f; // 건조 시간
    public float timeElapsed; // 경과 시간

    int numCollisionEvents = 0;

    void Start()
    {
        // 자식 오브젝트의 개수에 맞게 배열 초기화
        childObjects = new GameObject[transform.childCount];
        collisionEvents = new ParticleCollisionEvent[16]; // 충돌 이벤트 배열 초기화

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject; // 자식 오브젝트를 가져옴
            Debug.Log(childObjects[i]);
        }
    }

    void Update()
    {
        // isWet이 true이고, 아직 건조하지 않은 경우 경과 시간을 누적
        if (isWet && timeElapsed < DRY_TIME)
        {
            timeElapsed += Time.deltaTime;

            // 건조 시간이 지나면
            if (timeElapsed >= DRY_TIME)
            {
                // 자식 오브젝트의 메테리얼을 변경하고 isWet을 false로 설정
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[0]; // 기본 메테리얼로 변경
                }
                isWet = false;
                timeElapsed = 0f; // 경과 시간 초기화
                numCollisionEvents = 0;
                /*photonView.RPC("OnWat", RpcTarget.AllBuffered);*/
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // 충돌한 오브젝트가 파티클 시스템인지 확인
        if (other.GetComponent<ParticleSystem>() != null)
        {
            // 충돌 이벤트 수를 가져옴
            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);

            // 충돌 이벤트 수가 특정 값 이상인 경우에만 메테리얼 변경
            if (numCollisionEvents >= 5 && !isWet) // 충돌 이벤트가 5개 이상이고 아직 물에 젖지 않았을 때
            {
                Debug.Log("파티클과 충돌함! 충돌 이벤트 수: " + numCollisionEvents);
                // 자식 오브젝트의 메테리얼을 변경하고 isWet을 true로 설정
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[1]; // 물에 젖은 메테리얼로 변경
                }
                isWet = true;
               /* photonView.RPC("OnWat", RpcTarget.AllBuffered);*/
            }
        }
    }

    [PunRPC]
    private void OnWat()
    {
        if (isWet)
        {
            for (int i = 0; i < childObjects.Length - 1; i++)
            {
                childObjects[i].GetComponent<Renderer>().material = materials[1]; // 기본 메테리얼로 변경
            }
        }
        else
        {
            for (int i = 0; i < childObjects.Length - 1; i++)
            {
                childObjects[i].GetComponent<Renderer>().material = materials[0]; // 기본 메테리얼로 변경
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(isWet);
        }
        else
        {
            isWet = (bool)stream.ReceiveNext();
        }
    }
}