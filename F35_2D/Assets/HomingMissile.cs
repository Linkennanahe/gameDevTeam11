using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Tag of the enemy GameObject.
    public float speed = 2f;
    public float rotationSpeed = 70f;
    public float stoppingDistance = 1.5f; // The distance at which the AI will stop moving.

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D reference
    }

    private void FixedUpdate()
    {
        // Find the enemy GameObjects using the tag.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        if (enemies.Length > 0)
        {
            // Find the closest enemy to the AI.
            GameObject closestEnemy = FindClosestEnemy(enemies);
            if (closestEnemy != null)
            {
                Transform closestEnemyTransform = closestEnemy.transform;

                // Calculate the direction to the closest enemy.
                Vector2 directionToEnemy = closestEnemyTransform.position - transform.position;
                directionToEnemy.Normalize();

                // Calculate the angle between the AI's forward direction (transform.up) and the direction to the enemy.
                float angle = Vector3.Angle(transform.up, directionToEnemy);

                // Rotate the AI to face the enemy.
                float rotationSteer = Vector3.Cross(transform.up, directionToEnemy).z;
                rb.angularVelocity = rotationSteer * rotationSpeed;

                // Always apply force in the direction of the enemy.
                rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.deltaTime);

                // If the AI is close enough to the enemy, stop moving.
                if (directionToEnemy.magnitude < stoppingDistance)
                {
                    rb.velocity = Vector2.zero; // Stop movement.
                }
            }
        }
        else
        {
            // No enemies with the specified tag found.
            // You might want to add some behavior or handling here.
        }
    }

    // Find the closest enemy from an array of enemies.
    private GameObject FindClosestEnemy(GameObject[] enemies)
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
