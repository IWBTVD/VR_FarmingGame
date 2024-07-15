using UnityEngine;
using System.Collections;
using Autohand;


[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour
{
    private CarController m_Car;
    private Steering s;

    // public TestJoystick joystick;

    private void Awake()
    {
        m_Car = GetComponent<CarController>();
        s = new Steering();
        s.Start();
    }

    private void FixedUpdate()
    {
        s.UpdateValues();
        m_Car.Move(s.H, s.V, s.V, 0f);

    }
}

