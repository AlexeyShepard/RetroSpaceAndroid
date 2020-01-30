using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;

    public Vector3 Direction;

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, Screen.width), transform.position.y, transform.position.z);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, Screen.height), transform.position.z);

        Vector3 NormalDirection = new Vector3(Direction.x, Direction.z, Direction.y);

        transform.Translate(NormalDirection * speed * Time.deltaTime);
    }
}
