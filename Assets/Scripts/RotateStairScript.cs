using UnityEngine;

public class RotateStairScript : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public void Rotate()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
