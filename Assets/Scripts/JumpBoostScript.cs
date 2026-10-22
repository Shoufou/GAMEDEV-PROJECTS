using UnityEngine;
using UnityEngine.Events;

public class JumpBoostScript : MonoBehaviour
{
    public UnityEvent jumpBuff; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           jumpBuff?.Invoke();
           GetComponent<Renderer>().enabled = false;
           GetComponent<Collider>().enabled = false;
        }
    }
}
