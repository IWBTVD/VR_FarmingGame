using UnityEngine;
using System.Collections;
using Autohand;
using RoverExpedition;

[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car;
    private Steering s;

    private PhysicsGadgetJoystick joystick;
    private Animator animator;
    public Animator clawAnimator;

    public GameObject collectableObj;

    private void Awake()
    {
        m_Car = GetComponent<CarController>();
        joystick = GetComponentInChildren<PhysicsGadgetJoystick>();

        animator = GetComponent<Animator>();
        // clawAnimator = GetComponentInChildren<Animator>();

        s = new Steering(joystick);
        s.Start();
    }

    private void FixedUpdate()
    {
        s.UpdateValues();
        m_Car.Move(s.H, s.V, s.V, 0f);

        if (Input.GetKey(KeyCode.T))
        {
            animator.SetTrigger("ArmGrab");

            DetectCollectableObjectbyRange();

            if (collectableObj != null)
            {
                collectableObj.GetComponent<Collectable>().TryCollect();
            }
        }
    }

    public void ArmGrab()
    {
        clawAnimator.Play("Side_Claw_Grab");
    }

    private void DetectCollectableObjectbyRange()
    {
        float range = 2f;
        LayerMask layerMask = LayerMask.GetMask("CollectableLayer"); // 레이어 이름을 실제 사용하는 레이어로 변경
        Collider[] colls = Physics.OverlapSphere(transform.position, range, layerMask);

        foreach (var coll in colls)
        {
            Debug.Log("Detected: " + coll.name); // 디버그 로그 추가
            if (coll.TryGetComponent<Collectable>(out Collectable npc))
            {
                collectableObj = coll.gameObject;
                Debug.Log("Collectable Object Found: " + coll.name);
                return; // 첫 번째 발견된 객체로 설정하고 함수 종료
            }
        }

        // 범위 내에 Collectable 객체가 없는 경우
        collectableObj = null;
    }
}
