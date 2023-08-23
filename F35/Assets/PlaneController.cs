using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the movement
    public float rotationSpeed = 20f; // Speed of rotation
    public float speedIncreaseAmount = 0.1f; // Amount of speed increase per key press

    float forwardMovement = 1f; // Default forward movement
    float horizontalMovement; // Controls movement along the X-axis (left/right)

    float rotationDirection = 0f; // Controls rotation direction

    private void FixedUpdate()
    {
        MoveObject();
        RotateObject();
    }

    void MoveObject()
    {
        // Calculate the movement direction based on the current rotation
        Vector3 movementDirection = Quaternion.Euler(0f, transform.eulerAngles.y, 0f) * new Vector3(horizontalMovement, 0f, forwardMovement);

        // Apply movement based on the calculated direction
        Vector3 newPosition = transform.position + movementDirection * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    void RotateObject()
    {
        if (rotationDirection != 0f)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            forwardMovement += speedIncreaseAmount;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forwardMovement -= speedIncreaseAmount;
        }

        // Double the speed if "W" is pressed
        if (Input.GetKey(KeyCode.W))
        {
            forwardMovement = 2f;
        }

        // Halve the speed if "S" is pressed
        if (Input.GetKey(KeyCode.S))
        {
            forwardMovement = 0.5f;
        }

        forwardMovement = Mathf.Clamp(forwardMovement, 0.1f, 2f); // Clamp the speed between 0.1 and 2
    }

    void LateUpdate()
    {
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            forwardMovement = 1f; // Return to the original speed when keys are released
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementInput = movementValue.Get<Vector2>();
        horizontalMovement = movementInput.x;
    }

    void OnTurn(InputValue turnValue)
    {
        rotationDirection = turnValue.Get<float>();
    }
}
