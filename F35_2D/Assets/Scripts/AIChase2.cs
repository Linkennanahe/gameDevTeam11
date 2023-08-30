using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private Transform enemyTransform;
    private Transform playerTransform;
    private float sqrDistanceBetween;

    private Rigidbody2D rb; // Add a reference to the Rigidbody2D component

    private void Start()
    {
        enemyTransform = transform;
        playerTransform = player.transform;
        sqrDistanceBetween = distanceBetween * distanceBetween;

        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D reference
    }

    private void Update()
    {
        Vector2 direction = playerTransform.position - enemyTransform.position;
        float sqrDistance = direction.sqrMagnitude;

        if (sqrDistance < sqrDistanceBetween)
        {
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Move enemy based on the forward vector
            rb.MovePosition(rb.position + (Vector2)enemyTransform.up * speed * Time.deltaTime);

            if (direction != Vector2.zero)
            {
                RotateTowards(playerTransform.position, 10f); // Rotate towards the player with a specified rotation speed
            }
        }
    }

    // Rotate the enemy towards a target
    void RotateTowards(Vector3 targetPosition, float rotationSpeed)
    {
        Vector2 direction = targetPosition - enemyTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(Vector3.forward * angle);
        enemyTransform.rotation = Quaternion.RotateTowards(enemyTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
