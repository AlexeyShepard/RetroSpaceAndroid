using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseJoystick : MonoBehaviour
{
    public Image baseJoy;
    public Image stick;

    protected virtual string JoystickAreaTag {get; set;}

    protected JoystickAreaChecker JoystickAreaChecker;

    void Start()
    {
        FindJoystickAreaChecker();
        
        baseJoy.enabled = false;
        stick.enabled = false;
    }

    void FixedUpdate()
    {
        //if (IsJoystickEnabled()) ChangePosition();
        ChangePosition();
        //Debug.Log(JoystickAreaTag + " " + JoystickAreaChecker.JoystickTouch.phase);
    }

    protected void FindJoystickAreaChecker()
    {
        GameObject JoystickArea = GameObject.FindGameObjectWithTag(JoystickAreaTag);
        JoystickAreaChecker = JoystickArea.GetComponent<JoystickAreaChecker>();

        Debug.Log(JoystickArea.gameObject.name + " нашёлся!");
    }

    protected void ChangePosition()
    {
        /*if (WasActivated()) ActivateJoystick();
        if (IsActive()) MoveJoystick();
        if (WasDisabled()) HideJoystick();*/

        if (JoystickAreaChecker.IsMoving && JoystickAreaChecker.JoystickEnabled) MoveJoystick();      
        else if (JoystickAreaChecker.JoystickEnabled) ActivateJoystick();
        else HideJoystick();
    }

    protected bool WasActivated()
    {
        if (JoystickAreaChecker.JoystickTouch.phase == TouchPhase.Began)
        {
            //Debug.Log(JoystickAreaTag + " Began");
            return true;
        }
        else return false;
    }

    protected bool IsActive()
    {
        if (JoystickAreaChecker.JoystickTouch.phase == TouchPhase.Moved || JoystickAreaChecker.JoystickTouch.phase == TouchPhase.Stationary)
        {
            //Debug.Log(JoystickAreaTag + " Moved");
            return true;
        }
        else return false;
    }

    protected bool WasDisabled()
    {
        if (JoystickAreaChecker.JoystickTouch.phase == TouchPhase.Ended)
        {
            //Debug.Log(JoystickAreaTag + " Ended");
            return true;
        }
        else return false;
    }

    protected void ActivateJoystick()
    {
        baseJoy.gameObject.transform.position = GetJoystickPosition();
        stick.gameObject.transform.position = GetJoystickPosition();
        baseJoy.enabled = true;
        stick.enabled = true;
    }

    protected void HideJoystick()
    {
        baseJoy.enabled = false;
        stick.enabled = false;
    }

    protected void MoveJoystick()
    {
 
    }

    protected bool IsJoystickEnabled()
    {
        return JoystickAreaChecker.JoystickEnabled;
    }

    protected Vector2 GetJoystickPosition()
    {
        return JoystickAreaChecker.JoystickTouch.position;
    }


}
