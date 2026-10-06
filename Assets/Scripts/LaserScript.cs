using UnityEngine;
using System;
using UnityEngine.Events;


public class LaserScript : MonoBehaviour
{
    public UnityEvent playerHit;
    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.back * 10.0f * Time.deltaTime, Space.World);
    }

    //calls game manager event when a player hits it
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHit?.Invoke();
        }
    }
}
