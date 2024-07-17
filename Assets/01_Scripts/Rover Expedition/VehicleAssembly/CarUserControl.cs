using UnityEngine;
using System.Collections;
using Autohand;


[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car;
    private Steering s;

    private PhysicsGadgetJoystick joystick;
    private Animator animator;
    public Animator clawAnimator;

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
            //채집 함수 들어가야함

        }
    }

    public void ArmGrab()
    {
        clawAnimator.Play("Side_Claw_Grab");
    }




}

