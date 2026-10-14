using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
   
    //movement related fields
    private Vector2 input;
    [SerializeField] private float moveSpeed;
    private CharacterController controller;
    [SerializeField] private float smoothTime = 0.05f;
    private float currentVelocity;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private float velocity;
    [SerializeField] private float jumpPower;

    //player HP
    public int hp;
    public static UnityEvent<int> updateHealthUI = new UnityEvent<int>();
    public static UnityEvent<int> GameOver = new UnityEvent<int>();

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        hp = 100;
    }



    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }


    private void ApplyMovement()
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move *= moveSpeed;
        move.y = velocity;

        controller.Move(move * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if(controller.isGrounded && velocity < 0f)
        {
            velocity = -1;
        }

        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    
    private void Update()
    {
        ApplyGravity();
        ApplyMovement();
    }

    public void deductPlayerHP()
    {
        this.hp -= 5;
        updateHealthUI?.Invoke(hp);
        GameOver?.Invoke(hp);
    }

    public void resetPlayerHP()
    {
        this.hp = 100;
        updateHealthUI?.Invoke(hp);
    }

    public void resetBuffs()
    {
        this.jumpPower = 6.0f;
        this.moveSpeed = 10.0f;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!controller.isGrounded) return;

        velocity += jumpPower;
    }


   public void buffJump()
    {
        this.jumpPower += jumpPower * .2f;
    }

    public void buffSpeed()
    {
        this.moveSpeed += moveSpeed * .3f;
    }

    public void React()
    {
        controller.enabled = false;
        transform.position = new Vector3(-7.1f, 4.99f, -7.0547f);
        controller.enabled = true;
    }
}