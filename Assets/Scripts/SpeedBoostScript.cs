using UnityEngine;
using UnityEngine.Events;

public class SpeedBoostScript : MonoBehaviour
{
        public UnityEvent speedBuff;

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                speedBuff?.Invoke();
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }
        }
}
