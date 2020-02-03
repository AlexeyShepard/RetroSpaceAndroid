using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 50f;
    
    private InputHandler _InputHandler;
    private Rigidbody _RigidBody;
    
    private void Start()
    {
        _InputHandler = GetComponent<InputHandler>();
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();       
    }

    private void Move()
    {
        Vector2 JoystickDirection = _InputHandler.GetDirectionMovement();
        Vector3 CurrentDirection = Vector3.forward * JoystickDirection.y + Vector3.right * JoystickDirection.x;

        if (CurrentDirection != Vector3.zero)
        {
            Debug.Log("Движение произошло " + CurrentDirection.x + " " + CurrentDirection.z);
            _RigidBody.MovePosition(_RigidBody.transform.position + CurrentDirection * Time.fixedDeltaTime * Speed);
        } 
    }
}
