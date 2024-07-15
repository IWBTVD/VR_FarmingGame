using UnityEngine;
using UnityEngine.Events;

namespace Autohand
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class TestJoystick : MonoBehaviour
    {
        ConfigurableJoint joint;
        public bool invertX;
        public bool invertY;
        [Tooltip("For objects slightly off center. " +
            "\nThe minimum abs value required to return a value nonzero value\n " +
            "- if playRange is 0.1, you have to move the gadget 10% to get a result")]
        public float playRange = 0.05f;
        Vector2 xRange, zRange;
        Vector2 value;
        Vector3 jointRotation;
        Rigidbody body;

        // 키보드 입력에 사용할 변수
        public float keyboardSensitivity = 0.1f;
        private Vector2 keyboardInput;

        void Start()
        {
            joint = GetComponent<ConfigurableJoint>();
            body = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            // 키보드 입력 처리
            keyboardInput.x = Input.GetKey(KeyCode.LeftArrow) ? -keyboardSensitivity :
                              Input.GetKey(KeyCode.RightArrow) ? keyboardSensitivity : 0;

            keyboardInput.y = Input.GetKey(KeyCode.UpArrow) ? keyboardSensitivity :
                              Input.GetKey(KeyCode.DownArrow) ? -keyboardSensitivity : 0;

            // 디버그 로그 추가
            Debug.Log("Keyboard Input X: " + keyboardInput.x);
            Debug.Log("Keyboard Input Y: " + keyboardInput.y);

            xRange = new Vector2(joint.lowAngularXLimit.limit, joint.highAngularXLimit.limit);
            zRange = new Vector2(-joint.angularZLimit.limit, joint.angularZLimit.limit);
            jointRotation = joint.Angles();
            value = new Vector2(jointRotation.z / (zRange.x - zRange.y), jointRotation.x / (xRange.x - xRange.y)) * 2;

            // 키보드 입력을 기존 조이스틱 값에 더함
            value += keyboardInput;
        }

        public Vector2 GetValue()
        {
            if (Mathf.Abs(value.x) < playRange)
                value.x = 0;
            if (Mathf.Abs(value.y) < playRange)
                value.y = 0;

            value.x = invertX ? -value.x : value.x;
            value.y = invertY ? -value.y : value.y;
            return new Vector2(Mathf.Clamp(value.x, -1, 1), Mathf.Clamp(value.y, -1, 1));
        }

        public float GetKeyboardInputX()
        {
            return keyboardInput.x;
        }

        public float GetKeyboardInputY()
        {
            return keyboardInput.y;
        }
    }
}
