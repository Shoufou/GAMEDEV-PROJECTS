using UnityEngine;
using UnityEngine.Events;

public class FunWithLaserWinScript : MonoBehaviour
{
    public UnityEvent WinEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           WinEvent?.Invoke();
        }
    }
}
