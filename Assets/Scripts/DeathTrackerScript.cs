using UnityEngine;

public class DeathTracker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("Chracter").SendMessage("React");
        }
    }
}
