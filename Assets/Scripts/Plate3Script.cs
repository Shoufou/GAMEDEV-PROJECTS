using UnityEngine;

public class Plate3Script : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //code goes here
            GameObject.Find("Squigg (1)").SendMessage("Rotate");
        }
    }
}
