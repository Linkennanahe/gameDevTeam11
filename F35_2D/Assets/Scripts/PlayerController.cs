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
        Vector3 forward = transform.up;
        rb.MovePosition(rb.position + (Vector2)forward * CalculateSpeed() * Time.fixedDeltaTime);

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
