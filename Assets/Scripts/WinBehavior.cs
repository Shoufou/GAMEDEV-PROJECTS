using UnityEngine;

public class WinBehavior : MonoBehaviour
{
    [SerializeField] private Canvas WinFrame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //code goes here
            WinFrame.enabled = true;
        }
    }
}
