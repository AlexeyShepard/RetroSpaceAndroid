using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour
{
    public Image LeftJoystickArea;
    public Image RightJoystickArea;

    private GraphicRaycaster Raycaster;
    private PointerEventData PointerEventData;
    private EventSystem EventSystem;

    private JoystickAreaChecker LeftAreaChecker;
    private JoystickAreaChecker RightAreaChecker;

    void Start()
    {
        Raycaster = GetComponent<GraphicRaycaster>();
        EventSystem = GetComponent<EventSystem>();

        LeftAreaChecker = LeftJoystickArea.GetComponent<JoystickAreaChecker>();
        RightAreaChecker = RightJoystickArea.GetComponent<JoystickAreaChecker>();
    }

    void FixedUpdate()
    {
        if (IsScreenTouched()) HandleTouches(GetTouches());
        else
        {
            LeftAreaChecker.JoystickEnabled = false;
            RightAreaChecker.JoystickEnabled = false;
            LeftAreaChecker.JoystickTouch.phase = TouchPhase.Ended;
            RightAreaChecker.JoystickTouch.phase = TouchPhase.Ended;
        }

        if (LeftAreaChecker.JoystickEnabled) Debug.Log("Left true");
        else Debug.Log("Left false");

        if (RightAreaChecker.JoystickEnabled) Debug.Log("Right true");
        else Debug.Log("Right false");
    }

    private void HandleTouches(Touch[] CurrentTouches)
    {
        foreach(Touch Touch in CurrentTouches)
        {
            PointerEventData = new PointerEventData(EventSystem);
            PointerEventData.position = Touch.position;
            List<RaycastResult> results = new List<RaycastResult>();
            Raycaster.Raycast(PointerEventData, results);

            int i = 0;

            foreach(RaycastResult Result in results)
            {
                if (results[i].gameObject.tag == "LeftJoystickArea")
                {
                    LeftAreaChecker.JoystickEnabled = true;
                    LeftAreaChecker.JoystickTouch = Touch;
                }
                if (results[i].gameObject.tag == "RightJoystickArea")
                {
                    RightAreaChecker.JoystickEnabled = true;
                    RightAreaChecker.JoystickTouch = Touch;
                }

                i++;
            }           
        }
    }

    private Touch[] GetTouches()
    {
        Touch[] Touches = new Touch[Input.touchCount];

        switch (Input.touchCount)
        {
            case 1:
                {
                    return Input.touches;                  
                }
            case 2:
                {
                    for (int i = 0; i < SystemSettings.MaxTouchCount; i++)
                    {
                        Touches[i] = Input.touches[i];
                    }

                    return Touches;
                }
        }

        return Touches;
    }

    private bool IsScreenTouched()
    {
        if (Input.touchCount > 0)
        {
            return true;
        }

        return false;
    }
}
