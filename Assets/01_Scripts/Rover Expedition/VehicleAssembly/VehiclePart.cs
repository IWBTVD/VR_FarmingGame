using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePart : MonoBehaviour
{
    public PartSocket assembledSocket;
    public bool isAssembled => assembledSocket != null;

    public PartSocket hoveredSocket;

    private Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void OnGrab()
    {
        if(isAssembled)
        {
            assembledSocket.DettachPart(this);
        }
    }

    public void OnRelease()
    {
        if(!isAssembled && hoveredSocket != null)
        {
            hoveredSocket.AttachPart(this);
        }
    }

    public void OnAttach()
    {
        rigid.isKinematic = true;
    }

    public void OnDettach()
    {
        //rigid.isKinematic = false;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PartSocket socket))
        {
            hoveredSocket = socket;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(hoveredSocket != null)
        {
            if(hoveredSocket.gameObject == other.gameObject)
            {
                hoveredSocket = null;
            }
        }
    }
}
