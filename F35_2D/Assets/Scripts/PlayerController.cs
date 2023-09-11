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
    public AudioSource audioSource; // Reference to the AudioSource component for engine sound.

    public float normalSpeed;
    public float boost;
    public float reduceSpeed;
    public float rotationSpeed;

    // Max and min speeds
    public float maxSpeed = 5f;
    public float minSpeed = 1f;

    // Max audio pitch
    [SerializeField] private float maxPitch = 2.0f;

    // Acceleration and deceleration rates
    public float accelerationRate = 2f;
    public float decelerationRate = 4f;

    // Current speed
    private float currentSpeed;

    // Default pitch
    private float normalPitch;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.

        if (transform)
        {
            audioSource.Play(); // Play engine sound
        }

        currentSpeed = normalSpeed;
        normalPitch = audioSource.pitch;
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

        // Calculate pitch based on acceleration
        float targetPitch = CalculatePitch();
        float currentPitch = audioSource.pitch;

        // Gradually adjust the current pitch towards the target pitch
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, Time.deltaTime * (movement == Vector2.zero ? decelerationRate : accelerationRate));

        // Clamp the current pitch to stay within the desired bounds
        currentPitch = Mathf.Clamp(currentPitch, normalPitch, maxPitch);

        //change pitch
        audioSource.pitch = currentPitch;

        if (transform)
        {
            audioSource.loop = true; // Loop engine sound
        }
        else
        {
            audioSource.Pause();
        }
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

    float CalculatePitch()
    {
        float pitch = audioSource.pitch;

        if (movement.y > 0)
        {
            pitch *= boost / maxPitch;
        }
        else
        {
            pitch = normalPitch;
        }


        return pitch;
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
