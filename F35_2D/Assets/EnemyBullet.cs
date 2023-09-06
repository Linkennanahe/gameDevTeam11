using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletLifetime = 1.0f; // Adjust this value to control how long the bullet exists.
    private AudioSource enemybulletAudioSource; // Reference to the AudioSource component.
    private Renderer enemybulletRenderer; // Reference to the Renderer component.

    private bool hasCollided = false; // To ensure we handle the collision only once.

    private void Start()
    {
        // Automatically destroy the bullet after a certain time to prevent memory leaks.
        Destroy(gameObject, bulletLifetime);

        // Get the AudioSource component attached to the bullet.
        enemybulletAudioSource = GetComponent<AudioSource>();

        // Get the Renderer component attached to the bullet.
        enemybulletRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an object that should cause destruction.
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Ensure we handle the collision only once.
            if (!hasCollided)
            {
                hasCollided = true;

                // Perform any additional actions or effects on collision if needed.

                // Disable the bullet's renderer to make it invisible.
                enemybulletRenderer.enabled = false;

            }
        }
    }
}
