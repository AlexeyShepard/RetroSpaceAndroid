using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MovementBodySpeed = 10f;
    public float RotationBodySpeed = 180f;
    public float RotationBodySmooth = 0.2f;
    public bool IsShooting = false;
    public GameObject Tower;
    public GameObject Body;
    
    private InputHandler _InputHandler;
    private Rigidbody _RigidBody;
    private Vector3 _CurrentMovementDirection;
    private Vector3 _CurrentAimingDirection;
    
    private void Start()
    {
        _InputHandler = GetComponent<InputHandler>();
        _RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {       
        SetDirection();
        MoveBody();
        RotateBody();
        RotateTower();
    }

    private void SetDirection()
    {
        Vector2 JoystickDirection = _InputHandler.GetDirectionMovement();
        _CurrentMovementDirection = Vector3.forward * JoystickDirection.y + Vector3.right * JoystickDirection.x;

        JoystickDirection = _InputHandler.GetDirectionAiming();
        _CurrentAimingDirection = Vector3.forward * JoystickDirection.y + Vector3.right * JoystickDirection.x;
    }
    
    private void MoveBody()
    {
        if (IsMoving()) _RigidBody.MovePosition(CalculateMovement());
    }
   
    private bool IsMoving()
    {
        return _CurrentMovementDirection != Vector3.zero;
    }

    private Vector3 CalculateMovement()
    {
        return _RigidBody.transform.position + _CurrentMovementDirection * Time.fixedDeltaTime * MovementBodySpeed;
    }

    private void RotateBody()
    {
        if (IsMoving()) Body.transform.rotation = Quaternion.Lerp(Body.transform.rotation, CalculateBodyRotation(), RotationBodySmooth);
    }

    private Quaternion CalculateBodyRotation()
    {
        return Quaternion.LookRotation(_CurrentMovementDirection * Time.fixedDeltaTime);
    }  

    private void RotateTower()
    {
        if (IsTowerRotating())
        {
            Tower.transform.rotation = CalculateTowerRotation();
            IsShooting = true;
        }
        else IsShooting = false;
    }

    private bool IsTowerRotating()
    {
        return _CurrentAimingDirection != Vector3.zero;
    }

    private Quaternion CalculateTowerRotation()
    {
        return Quaternion.LookRotation(_CurrentAimingDirection * Time.fixedDeltaTime);
    }
}
