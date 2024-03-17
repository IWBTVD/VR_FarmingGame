using Photon.Pun;
using UnityEngine;
using Photon;

public class ChangeMaterial : MonoBehaviourPunCallbacks, IPunObservable
{
    public Material[] materials;
    private GameObject[] childObjects;
    private ParticleCollisionEvent[] collisionEvents; // �浹 �̺�Ʈ �迭

    public bool isWet;
    private float DRY_TIME = 3f; // ���� �ð�
    public float timeElapsed; // ��� �ð�

    int numCollisionEvents = 0;

    void Start()
    {
        // �ڽ� ������Ʈ�� ������ �°� �迭 �ʱ�ȭ
        childObjects = new GameObject[transform.childCount];
        collisionEvents = new ParticleCollisionEvent[16]; // �浹 �̺�Ʈ �迭 �ʱ�ȭ

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject; // �ڽ� ������Ʈ�� ������
            Debug.Log(childObjects[i]);
        }
    }

    void Update()
    {
        // isWet�� true�̰�, ���� �������� ���� ��� ��� �ð��� ����
        if (isWet && timeElapsed < DRY_TIME)
        {
            timeElapsed += Time.deltaTime;

            // ���� �ð��� ������
            if (timeElapsed >= DRY_TIME)
            {
                // �ڽ� ������Ʈ�� ���׸����� �����ϰ� isWet�� false�� ����
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[0]; // �⺻ ���׸���� ����
                }
                isWet = false;
                timeElapsed = 0f; // ��� �ð� �ʱ�ȭ
                numCollisionEvents = 0;
                /*photonView.RPC("OnWat", RpcTarget.AllBuffered);*/
            }
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        // �浹�� ������Ʈ�� ��ƼŬ �ý������� Ȯ��
        if (other.GetComponent<ParticleSystem>() != null)
        {
            // �浹 �̺�Ʈ ���� ������
            numCollisionEvents = other.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, collisionEvents);

            // �浹 �̺�Ʈ ���� Ư�� �� �̻��� ��쿡�� ���׸��� ����
            if (numCollisionEvents >= 5 && !isWet) // �浹 �̺�Ʈ�� 5�� �̻��̰� ���� ���� ���� �ʾ��� ��
            {
                Debug.Log("��ƼŬ�� �浹��! �浹 �̺�Ʈ ��: " + numCollisionEvents);
                // �ڽ� ������Ʈ�� ���׸����� �����ϰ� isWet�� true�� ����
                for (int i = 0; i < childObjects.Length - 1; i++)
                {
                    childObjects[i].GetComponent<Renderer>().material = materials[1]; // ���� ���� ���׸���� ����
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
                childObjects[i].GetComponent<Renderer>().material = materials[1]; // �⺻ ���׸���� ����
            }
        }
        else
        {
            for (int i = 0; i < childObjects.Length - 1; i++)
            {
                childObjects[i].GetComponent<Renderer>().material = materials[0]; // �⺻ ���׸���� ����
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