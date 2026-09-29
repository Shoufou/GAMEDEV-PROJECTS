using UnityEngine;

public class Plate1Script : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //code goes here
            GameObject.Find("Stairs").SendMessage("Rotate");
        }
    }
}
