using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public float speed = 2f;
    public float rotationSpeed = 70f;
    public float distanceBetween;
    public float stoppingDistance = 1.5f; // The distance at which the AI will stop moving.

    private Transform enemyTransform;
    private Transform playerTransform;
    private float sqrDistanceBetween;

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    private void Start()
    {
        enemyTransform = transform;
        sqrDistanceBetween = distanceBetween * distanceBetween;

        rb = GetComponent<Rigidbody2D>(); // Initialize the Rigidbody2D reference
    }

    private void FixedUpdate()
    {
        // Find the player GameObject using the tag.
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        if (players.Length > 0)
        {
            // Find the closest player to the enemy.
            GameObject closestPlayer = FindClosestPlayer(players);
            if (closestPlayer != null)
            {
                playerTransform = closestPlayer.transform;

                // Calculate the direction to the player.
                Vector2 directionToPlayer = playerTransform.position - enemyTransform.position;
                directionToPlayer.Normalize();

                // Calculate the angle between the AI's forward direction (transform.up) and the direction to the player.
                float angle = Vector3.Angle(transform.up, directionToPlayer);

                // Rotate the AI to face the player.
                float rotationSteer = Vector3.Cross(transform.up, directionToPlayer).z;
                rb.angularVelocity = rotationSteer * rotationSpeed;
                rb.AddForce(transform.up * speed, ForceMode2D.Force);

                // If the AI is facing the player, move towards the player.
                if (angle < 20f)
                {
                    // Distance barrier between enemy and player
                    if (directionToPlayer.sqrMagnitude < sqrDistanceBetween)
                    {
                        rb.velocity = Vector2.zero; // Stop movement.
                    }

                    // If the AI is close enough to the player, stop moving.
                    if (directionToPlayer.magnitude < stoppingDistance)
                    {
                        rb.velocity = Vector2.zero; // Stop movement.
                    }
                }
                else
                {
                    rb.velocity = Vector2.zero; // Stop movement if not facing the player.
                }
            }
        }
        else
        {
            // No players with the specified tag found.
            // You might want to add some behavior or handling here.
        }
    }

    // Find the closest player from an array of players.
    private GameObject FindClosestPlayer(GameObject[] players)
    {
        GameObject closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (var player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }
}
