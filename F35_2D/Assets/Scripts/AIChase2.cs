using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    // Tag of the player GameObject.
    public string playerTag = "Player";

    // Speed at which the AI moves.
    public float speed = 2f;

    // Speed at which the AI rotates.
    public float rotationSpeed = 70f;

    // Minimum distance between the AI and the player.
    public float distanceBetween;

    // The distance at which the AI will stop moving.
    public float stoppingDistance = 1.5f;

    // Reference to the AI's transform.
    private Transform enemyTransform;

    // Reference to the player's transform.
    private Transform playerTransform;

    // The square of the distance between AI and player.
    private float sqrDistanceBetween;

    // Reference to the Rigidbody2D component.
    private Rigidbody2D rb;

    private void Start()
    {
        // Get a reference to the AI's transform.
        enemyTransform = transform;

        // Calculate the square of the distance between.
        sqrDistanceBetween = distanceBetween * distanceBetween;

        // Initialize the Rigidbody2D reference.
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Find all GameObjects with the specified tag.
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);

        if (players.Length > 0)
        {
            // Find the closest player to the enemy.
            GameObject closestPlayer = FindClosestPlayer(players);
            if (closestPlayer != null)
            {
                // Get the player's transform.
                playerTransform = closestPlayer.transform;

                // Calculate the direction to the player.
                Vector2 directionToPlayer = playerTransform.position - enemyTransform.position;
                directionToPlayer.Normalize();

                // Calculate the angle between the AI's forward direction (transform.up) and the direction to the player.
                float angle = Vector3.Angle(transform.up, directionToPlayer);

                // Rotate the AI to face the player.
                float rotationSteer = Vector3.Cross(transform.up, directionToPlayer).z;
                rb.angularVelocity = rotationSteer * rotationSpeed;
                
                // Move the AI.
                rb.AddForce(transform.up * speed, ForceMode2D.Force);

                // If the AI is facing the player, move towards the player.
                if (angle < 20f)
                {
                    // Distance barrier between enemy and player.
                    if (directionToPlayer.sqrMagnitude < sqrDistanceBetween)
                    {
                        // Stop movement.
                        rb.velocity = Vector2.zero;
                    }

                    // If the AI is close enough to the player, stop moving.
                    if (directionToPlayer.magnitude < stoppingDistance)
                    {
                        // Stop movement.
                        rb.velocity = Vector2.zero;
                    }
                }
                else
                {
                    // Stop movement if not facing the player.
                    rb.velocity = Vector2.zero;
                }
            }
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
