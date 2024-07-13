using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Unity.VisualScripting;
public class VehicleMove : MonoBehaviour
{

    private PhysicsGadgetJoystick physicsGadgetJoystick;

    public GameObject carwheelL;
    public GameObject carwheelR;
    void Start()
    {

        physicsGadgetJoystick = GetComponentInChildren<PhysicsGadgetJoystick>();
    }

    void Update()
    {
        // MoveCar(physicsGadgetJoystick.GetValue());
        if (Input.GetKey(KeyCode.W))
        {
            MoveCarFrontBack(Vector2.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveCarFrontBack(Vector2.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveCarLeftRight(Vector2.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveCarLeftRight(Vector2.right);
        }
    }
    void MoveCarFrontBack(Vector2 vector2)
    {
        //vector3의 방향으로 현제 차를 움직이는 코드

        transform.Translate(vector2.x, 0, vector2.y);

    }

    void MoveCarLeftRight(Vector2 vector2)
    {
        //자동차 바퀴를 움직이는 코드 나중에 바퀴 모델은 info에서 가져와야함.

        Debug.Log(vector2.x);
        if (vector2.x > 0)
        {
            carwheelL.transform.Rotate(0, 10, 0);
            carwheelR.transform.Rotate(0, 10, 0);
        }

    }


}
