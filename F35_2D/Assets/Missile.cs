using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    // Adjust this value to control how long the missile exists.
    public float missileLifetime = 5.0f;

    // Adjust the damage the missile deals on impact.
    public int missileDamage = 10;

    // Reference to the AudioSource component.
    private AudioSource missileAudioSource;

    // Reference to the Renderer component.
    private Renderer missileRenderer;

    // Make sure only handle the collision once.
    private bool hasCollided = false;

    private void Start()
    {
        // Automatically destroy the missile after a certain time to prevent memory leaks.
        Destroy(gameObject, missileLifetime);

        // Get the AudioSource component attached to the missile.
        missileAudioSource = GetComponent<AudioSource>();

        // Get the Renderer component attached to the missile.
        missileRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the missile collides with an object that should cause destruction.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Make sure only handle the collision once.
            if (!hasCollided)
            {
                hasCollided = true;

                // Disable the missile's renderer to make it invisible.
                missileRenderer.enabled = false;

                // Stop playing the audio source.
                if (missileAudioSource != null)
                {
                    missileAudioSource.Stop();
                }

                // Destroy the missile GameObject on collision.
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            // Make sure only handle the collision once.
            if (!hasCollided)
            {
                hasCollided = true;

                // Perform any actions or effects for colliding with obstacles.
                // For example, you could add an explosion effect or damage the obstacle.

                // Disable the missile's renderer to make it invisible.
                missileRenderer.enabled = false;

                // Stop playing the audio source.
                if (missileAudioSource != null)
                {
                    missileAudioSource.Stop();
                }

                // Destroy the missile GameObject on collision.
                Destroy(gameObject);
            }
        }
    }
}
