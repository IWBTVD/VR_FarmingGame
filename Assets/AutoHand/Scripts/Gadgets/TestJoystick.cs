using UnityEngine;
using UnityEngine.Events;

namespace Autohand
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class TestJoystick : PhysicsGadgetJoystick
    {
        public float GetKeyboardInputX()
        {
            return value.x;
        }

        public float GetKeyboardInputY()
        {
            return value.y;
        }
    }
}
