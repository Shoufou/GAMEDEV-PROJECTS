using UnityEngine;

public class StairPingPong : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;      
    [SerializeField] private float maxDistance = 5.0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float currentDistance = Mathf.PingPong(Time.time * speed, maxDistance);

        transform.position = new Vector3(startPosition.x + currentDistance, startPosition.y, startPosition.z);
    }
}
