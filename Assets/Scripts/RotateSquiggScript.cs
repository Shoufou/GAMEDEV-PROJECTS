using UnityEngine;

public class RotateSquiggScript : MonoBehaviour
{
    private Vector3 objectCenter;
    public float rotationSpeed = 50f;

    public void Rotate()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}