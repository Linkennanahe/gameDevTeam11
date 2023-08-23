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

    public float normalSpeed;
    public float boost;
    public float reduceSpeed;
    public float rotationSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        // Calculate the forward vector based on the object's current rotation
        Vector3 forward = transform.up;

        // Move the object based on the forward vector
        rb.MovePosition(rb.position + (Vector2)forward * Time.fixedDeltaTime);

        // Update animator parameters based on movement
        if (movement == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
        }
        else if (movement.x < 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isMovingRight", false);
            transform.Translate(movement * Time.deltaTime);
        }
        else if (movement.x > 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", true);
            transform.Translate(movement * Time.deltaTime);
        }
        else if (movement.y > 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
            transform.Translate(movement * boost * Time.deltaTime);
        }
        else if (movement.y < 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isMovingLeft", false);
            animator.SetBool("isMovingRight", false);
            transform.Translate(movement * reduceSpeed * Time.deltaTime);

        }

    }

    void OnMove(InputValue movementValue)
    {
        movement = movementValue.Get<Vector2>();
    }

    void Update()
    {
        // Check for A and D key presses to rotate the object
        if (Keyboard.current.aKey.isPressed)
        {
            RotateAndMove(10f);

        }
        else if (Keyboard.current.dKey.isPressed)
        {
            RotateAndMove(-10f);
  
        }
    }

    // Rotate the object by the specified angle
    void RotateAndMove(float angle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
