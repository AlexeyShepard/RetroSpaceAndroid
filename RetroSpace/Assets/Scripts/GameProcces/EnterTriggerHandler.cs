using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerHandler : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnterTriggerEnter;
    
    private void OnTriggerEnter(Collider other)
    {
        OnEnterTriggerEnter.Invoke();       
    }
}
