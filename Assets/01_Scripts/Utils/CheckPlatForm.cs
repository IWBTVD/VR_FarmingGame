using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CheckPlatForm
{
    public enum Platform
    {
        PC,
        XR,
    }

    public static bool IsVRPlatForm()
    {
        return XRSettings.enabled;
    }

    public static bool IsPCPlatForm()
    {
        return !IsVRPlatForm();
    }
}