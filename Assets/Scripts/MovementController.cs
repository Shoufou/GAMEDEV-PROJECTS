using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] CharacterController controller;
    [SerializeField] float gForce = 5f;
    Vector2 moveInput;
    float verticalVelocity = 0f;

    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 horizontalMove = move * moveSpeed;

        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gForce * Time.deltaTime;
        }

        Vector3 finalMovement = new Vector3(horizontalMove.x, verticalVelocity, horizontalMove.z);
        controller.Move(finalMovement * Time.deltaTime);
    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

    }

    public void React()
    {
        controller.enabled = false;
        transform.position = new Vector3(-7.1f, 4.99f, -7.0547f);
        controller.enabled = true;
    }
}