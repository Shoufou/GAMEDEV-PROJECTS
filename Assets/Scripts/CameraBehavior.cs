using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class CameraBehavior : MonoBehaviour
{

    [SerializeField] private Transform player;
    public float mouseSensitivity = 20f;
    float cameraverticalRotation = 0;
    private Vector2 lookInput;

    //ball behaviors
    [SerializeField] private Rigidbody ballPrefab;
    [SerializeField] private Collider playerCollider; // Chracter's CharacterController
    [SerializeField] private float throwForce = 15f;
    [SerializeField] private float spawnDistance = 1f;
    [SerializeField] private float lifetime = 10f;

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;
        Rigidbody ball = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        // Stop the ball from colliding with the thrower
        Collider ballCol = ball.GetComponent<Collider>();
        if (playerCollider != null && ballCol != null)
            Physics.IgnoreCollision(ballCol, playerCollider);

        ball.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

        Destroy(ball.gameObject, lifetime);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float inputX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float inputY = lookInput.y * mouseSensitivity * Time.deltaTime;

        cameraverticalRotation -= inputY;
        cameraverticalRotation = Mathf.Clamp(cameraverticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraverticalRotation;
        
        player.Rotate(Vector3.up * inputX);
    }
}
