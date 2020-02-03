using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject LeftJoystick;

    public GameObject RightJoystick;

    private FloatingJoystick _LeftJoystick;

    private FloatingJoystick _RightJoystick;

    private void Start()
    {
        _LeftJoystick = LeftJoystick.GetComponent<FloatingJoystick>();
        _RightJoystick = RightJoystick.GetComponent<FloatingJoystick>();
    }

    public Vector2 GetDirectionMovement()
    {
        return _LeftJoystick.Direction;
    }

    public Vector2 GetDirectionAiming()
    {
        return _RightJoystick.Direction;
    }
}
