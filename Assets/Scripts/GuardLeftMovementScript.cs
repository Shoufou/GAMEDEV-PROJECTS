using UnityEngine;

public class GuardLeftMovementScript : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.right; // axis to move along
    [SerializeField] private float distance = 30f;               // how far it travels
    [SerializeField] private float speed = 0.2f;

    private Rigidbody rb;
    private Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = rb.position;
    }

    void FixedUpdate()
    {
        Vector3 target = new Vector3(startPos.x - Mathf.PingPong(Time.time / speed, distance), startPos.y, startPos.z);
        rb.MovePosition(target);
    }
}