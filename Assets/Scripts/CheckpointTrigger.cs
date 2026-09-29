using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //code goes here
            print("Entered zone, combat start!");
        }

        Instantiate(obj, spawnPoint);
    }

    private void OnTriggerExit(Collider other)
    {
        obj.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
