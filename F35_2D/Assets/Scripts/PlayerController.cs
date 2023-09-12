using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    AudioSource audioSource; // Reference to the AudioSource component for engine sound.

    public float normalSpeed;
    public float boost;
    public float reduceSpeed;
    public float rotationSpeed;

    // Max and min speeds
    public float maxSpeed = 5f;
    public float minSpeed = 1f;

    // Acceleration and deceleration rates
    public float accelerationRate = 2f;
    public float decelerationRate = 4f;

    // Current speed
    private float currentSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.

        currentSpeed = normalSpeed;
    }

    void FixedUpdate()
    {
        Vector3 forward = transform.up;
        rb.MovePosition(rb.position + (Vector2)forward * currentSpeed * Time.fixedDeltaTime);

        UpdateAnimator();
    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }

    void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            Rotate(rotationSpeed);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            Rotate(-rotationSpeed);
        }

        // Calculate the target speed based on input
        float targetSpeed = CalculateSpeed();

        // Gradually adjust the current speed towards the target speed
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * (movement == Vector2.zero ? decelerationRate : accelerationRate));

        // Clamp the current speed to stay within the desired bounds
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    float CalculateSpeed()
    {
        float speed = normalSpeed;

        if (movement.y > 0)
        {
            speed *= boost;
        }
        else if (movement.y < 0)
        {
            speed *= reduceSpeed;
        }

        return speed;
    }

    void Rotate(float rotationAmount)
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + rotationAmount);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        animator.SetBool("isMoving", movement != Vector2.zero);

        if (movement.x < 0)
        {
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isMovingRight", false);
        }
        else if (movement.x > 0)
        {
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", true);
        }
        else
        {
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
        }
    }
}
