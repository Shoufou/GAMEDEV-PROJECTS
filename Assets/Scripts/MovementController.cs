using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
   
    //movement related fields
    private Vector2 input;
    private Vector3 direction;
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
        direction = new Vector3(input.x, 0.0f, input.y);
    }

    private void ApplyRotation()
    {
        if (input.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void ApplyMovement()
    {
        controller.Move(direction * moveSpeed * Time.deltaTime);
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
        direction.y = velocity;
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