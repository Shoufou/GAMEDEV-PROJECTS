using UnityEngine;

public class RotateLBlockScript : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public void Rotate()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
