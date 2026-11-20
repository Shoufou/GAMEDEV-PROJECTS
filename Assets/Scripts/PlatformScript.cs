using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.right; // axis to move along
    [SerializeField] private float distance = 5f;               // how far it travels
    [SerializeField] private float speed = 2f;

    private Rigidbody rb;
    private Vector3 startPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = rb.position;
    }

    void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.time * speed / distance, 1f);
        float offset = Mathf.SmoothStep(0f, distance, t);
        rb.MovePosition(startPos + direction.normalized * offset);
    }
}