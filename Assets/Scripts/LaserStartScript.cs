using UnityEngine;
using System;
using UnityEngine.Events;

public class LaserStartScript : MonoBehaviour
{
    public UnityEvent StartCountdownEvent;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCountdownEvent?.Invoke();
        }
    }
}
