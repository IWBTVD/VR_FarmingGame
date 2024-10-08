using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using EPOOutline;


public class Pickaxe : MonoBehaviour, ICanBreak, IToolBase
{
    private int _damage = 200;
    public int Damage => _damage;

    private int _toolID;  // toolID 값을 저장할 필드
    private Outlinable _outlinable;

    public int toolID
    {
        get => _toolID;
        set => _toolID = value;
    }

    private BreakableObject _breakableObject;
    public BreakableObject BreakableObject
    {
        get => _breakableObject;
        set => _breakableObject = value;
    }


    void Awake()
    {
        _outlinable = GetComponent<Outlinable>();
        toolID = 1;
    }

    void Start()
    {
        _outlinable.OutlineParameters.Enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //일정 가속도 이상에서만 호출
        Debug.Log("PickaxeCollided " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= 1f)
        {
            if (collision.gameObject.tag == "Obstacle")
            {
                var breakable = collision.gameObject.GetComponentInParent<IBreakable>();

                if (breakable != null)
                {
                    Debug.Log("Mining Performed");
                    breakable.OnBreakWithPickaxe(_damage);
                }
            }
        }
    }

    public void DoAction()
    {
        if (_breakableObject == null) return;
        _breakableObject.OnBreakWithAxe(_damage);
    }

    public void SetBreakableObject(BreakableObject target)
    {
        _breakableObject = target;
    }
}
