using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{

    public GameObject Touch;

    private Vector3 TouchTarget;

    private CharacterController CharacterController;

    public GameObject Cube;
    
    void Start()
    {
        Touch.transform.position = transform.position;
        CharacterController = Cube.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 TouchPosition = Input.mousePosition;

            TouchTarget = TouchPosition - transform.position;

            if(TouchTarget.magnitude < 300)
            {
                Touch.transform.position = TouchPosition;
                CharacterController.Direction = TouchTarget;
            }
        }
        else
        {
            Touch.transform.position = transform.position;
            CharacterController.Direction = new Vector3(0, 0, 0);
        }
    }
}
