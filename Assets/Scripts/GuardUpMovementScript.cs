using UnityEngine;

public class GuardUpMovementScript : MonoBehaviour
{
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
        Vector3 target = new Vector3(startPos.x, startPos.y, startPos.z + Mathf.PingPong(Time.time / speed, distance));
        rb.MovePosition(target);
    }
}