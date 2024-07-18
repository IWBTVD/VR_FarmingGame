using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SocketType
{
    A,
    B,
    Both
}

public enum AssemblyDirection
{
    /// <summary>
    /// 전방만 결합가능
    /// </summary>
    Forward,
    /// <summary>
    /// 후방만 결합가능
    /// </summary>
    Backward,
    Left,
    Right,
    /// <summary>
    /// 전방후방
    /// </summary>
    ForwardAndBackward,
    LeftAndRight,
    FourDirection,
    Up,
    Down,
    UpAndDown,
    AllButDown,
    AllButUp,
    All
}

public class PartSocket : MonoBehaviour
{
    [SerializeField] private VehicleChassis chassis;
    [SerializeField] VehiclePart m_parentPart;
    [SerializeField] private SocketType m_socketType;
    public SocketType SocketType { get => m_socketType; set => m_socketType = value;}

    [SerializeField] private AssemblyDirection m_assemblyDirection;

    [SerializeField] private VehiclePart m_attachedPart;

    private void Start()
    {
        if (chassis == null) chassis = GetComponentInParent<VehicleChassis>();
    }

    public void AttachPart(VehiclePart part)
    {
        chassis.AssemblePart(part);
        part.transform.SetParent(transform);
        part.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        part.OnAttach();
        m_attachedPart = part;
    }

    public void DettachPart(VehiclePart part)
    {
        chassis.DisassemblePart(part);
        if(part.transform.parent == transform)
        {
            part.transform.SetParent(null);
        }

        part.OnDettach();
        m_attachedPart = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌" + other.name);
    }
}
