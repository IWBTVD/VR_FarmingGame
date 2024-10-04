using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;


public class Pickaxe : MonoBehaviour, ICanBreak
{
    private int _damage = 200;
    public int Damage => _damage;

    private int _toolID;  // toolID 값을 저장할 필드

    public int toolID
    {
        get => _toolID;
        set => _toolID = value;
    }

    void Awake()
    {
        toolID = 1;
    }

    public int GetID()
    {
        Debug.Log(toolID);
        return toolID;
    }


    public void DoAction(BreakableObject targetObstacle)
    {
        targetObstacle.OnBreakWithAxe(_damage);
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
}
