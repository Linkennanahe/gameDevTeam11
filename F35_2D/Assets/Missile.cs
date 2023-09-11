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

    // Explosion sound
    public AudioClip explosionSound;

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
            HandleCollision();
        }
        else if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
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

            // Play the explosion sound if available.
            if (explosionSound != null)
            {
                // Play the explosion sound at the missile's position.
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            // Delay the destruction of the missile GameObject.
            StartCoroutine(DestroyMissile());
        }
    }

    private IEnumerator DestroyMissile()
    {
        // Wait for a short period of time to allow the explosion sound to play.
        yield return new WaitForSeconds(0.2f); // Adjust the time as needed.

        // Destroy the missile GameObject.
        Destroy(gameObject);
    }
}
