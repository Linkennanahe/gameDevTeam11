using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Adjust this value to control how long the bullet exists.
    public float bulletLifetime = 2.0f;

    // Reference to the AudioSource component.
    private AudioSource bulletAudioSource;

    // Reference to the Renderer component.
    private Renderer bulletRenderer;

    // To ensure we handle the collision only once.
    private bool hasCollided = false;

    private void Start()
    {
        // Automatically destroy the bullet after a certain time to prevent memory leaks.
        Destroy(gameObject, bulletLifetime);

        // Get the AudioSource component attached to the bullet.
        bulletAudioSource = GetComponent<AudioSource>();

        // Get the Renderer component attached to the bullet.
        bulletRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an object that should cause destruction.
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Ensure we handle the collision only once.
            if (!hasCollided)
            {
                hasCollided = true;

                // Perform any additional actions or effects on collision if needed.

                // Disable the bullet's renderer to make it invisible.
                bulletRenderer.enabled = false;

                // Stop playing the audio source.
                if (bulletAudioSource != null)
                {
                    bulletAudioSource.Stop();
                }
            }
        }
    }
}
