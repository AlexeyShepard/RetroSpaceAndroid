﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    float Smoothing = 10f;

    Vector3 Offset;

    private void Start()
    {
        Offset = gameObject.transform.position - Player.transform.position;
    }

    private void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Player.transform.position + Offset, Smoothing);   
    }
}
