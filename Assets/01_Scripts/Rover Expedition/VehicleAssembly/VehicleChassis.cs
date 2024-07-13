using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleChassis : MonoBehaviour
{
    private float power = 0;
    private float motorSpeed = 0;
    private float wheelPower = 0;
    private float containerSize = 0;

    public float Power { get => power; set => power = value; }
    public float MotorSpeed { get => motorSpeed; set => motorSpeed = value; }
    public float WheelPower { get => wheelPower; set => wheelPower = value; }
    public float ContainerSize { get => containerSize; set => containerSize = value; }

    public void AddPower(float power)
    {
        this.power += power;
    }

    public void AddMotorSpeed(float speed)
    {
        this.motorSpeed += speed;
    }

    public void AddWheelPower(float power)
    {
        this.wheelPower += power;
    }

    public void AddContainerSize(float size)
    {
        this.containerSize += size;
    }
}
