using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase2 : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject.
    public float speed = 2f;
    public float rotationSpeed = 70f;
    public float distanceBetween;

    private Transform enemyTransform;
    private Transform playerTransform;
    private float sqrDistanceBetween;

    private Rigidbody2D rb; // Add a reference to the Rigidbody2D component

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

                // Missile movement
                Vector2 offset = (playerTransform.position - enemyTransform.position); // Direction enemyTransform needs to face
                Vector2 direction = offset.normalized; // Normalized direction
                rb.velocity = speed * Time.fixedDeltaTime * direction; // Enemy velocity
                float rotationSteer = Vector3.Cross(transform.up, direction).z; // Cross product of player and enemy vector
                rb.angularVelocity = rotationSteer * rotationSpeed; // Enemy rotation of Y-axis to face player

                // Distance barrier between enemy and player
                if (offset.sqrMagnitude < sqrDistanceBetween)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePosition;
                }
                else
                {
                    rb.constraints = RigidbodyConstraints2D.None;
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
