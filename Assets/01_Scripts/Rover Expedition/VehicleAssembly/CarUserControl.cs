using UnityEngine;
using System.Collections;
using Autohand;
using RoverExpedition;
using Unity.VisualScripting;

[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car;
    private Steering s;

    private TestJoystick joystick;
    private Animator animator;
    public Animator clawAnimator;

    public GameObject collectableObj;
    public float range = 2f;

    private void Awake()
    {
        m_Car = GetComponent<CarController>();
        joystick = GetComponentInChildren<TestJoystick>();

        animator = GetComponent<Animator>();
        // clawAnimator = GetComponentInChildren<Animator>();

        s = new Steering(joystick);
        s.Start();
    }

    private void FixedUpdate()
    {
        s.UpdateValues();
        m_Car.Move(s.H, s.V, s.V, 0f);

        // 가장 가까운 Collectable 객체를 찾아서 저장
        collectableObj = DetectCollectableObjectbyRange();

        if (Input.GetKey(KeyCode.T))
        {
            animator.SetTrigger("ArmGrab");

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

    private GameObject DetectCollectableObjectbyRange()
    {

        LayerMask layerMask = LayerMask.GetMask("CollectableLayer"); // 레이어 이름을 실제 사용하는 레이어로 변경
        Collider[] colls = Physics.OverlapSphere(transform.position, range, layerMask);

        foreach (var coll in colls)
        {
            if (coll.TryGetComponent<Collectable>(out Collectable npc))
            {
                return coll.gameObject; // 첫 번째 발견된 객체로 설정하고 함수 종료
            }
        }

        // 범위 내에 Collectable 객체가 없는 경우
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
