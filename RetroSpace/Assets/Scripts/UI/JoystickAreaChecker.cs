using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAreaChecker : MonoBehaviour
{
    [HideInInspector]
    public bool JoystickEnabled;

    [HideInInspector]
    public Touch JoystickTouch;

    [HideInInspector]
    public bool IsMoving;

    private Touch OldTouch;

    public void FixedUpdate()
    {
        Debug.Log(JoystickTouch.position + " " + OldTouch.position);
        if (JoystickTouch.position == OldTouch.position) IsMoving = false;
        else IsMoving = true;
        OldTouch = JoystickTouch;        
    }

}
