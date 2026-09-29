using UnityEngine;

public class Plate2Script : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("L Block").SendMessage("Rotate");
        }
    }
}
