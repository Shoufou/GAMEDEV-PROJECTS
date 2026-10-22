using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class CheckSphereScript : MonoBehaviour
{
    public float sphereRadius = 5f;
    public LayerMask layertoCheck;
    public UnityEvent gameOver;


    private bool doIseePlayer = false;
    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereRadius, layertoCheck))
        {
            Debug.Log("Detected!");
            if(doIseePlayer == false)
            {
                doIseePlayer=true;
                StealthGameManagerBehavior.instance.guardsWhoSeePlayer++;
            }
        }

        else
        {
            if (doIseePlayer)
            {
                doIseePlayer = false;
                StealthGameManagerBehavior.instance.guardsWhoSeePlayer--;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }

    


}
