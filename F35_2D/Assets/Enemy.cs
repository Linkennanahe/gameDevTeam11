using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int hitCount = 0; // Number of hits the enemy has taken.
    private Animator animator; // Reference to the Animator component.
    private Collider2D enemyCollider; // Reference to the Collider2D component.
    private Rigidbody2D rb; // Reference to the Rigidbody2D component.
    private AIWeaponSystem aiWeaponSystem; // Reference to the AI weapon system script.
    private AudioSource audioSource; // Reference to the AudioSource component for death sound.

    public AudioClip deathSound; // Assign the death sound in the Inspector.

    private void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component.
        enemyCollider = GetComponent<Collider2D>(); // Get the Collider2D component.
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component.
        aiWeaponSystem = GetComponent<AIWeaponSystem>(); // Get the AI weapon system script.
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component.

        // Set the death sound for the AudioSource component.
        audioSource.clip = deathSound;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player Weapon")) // Assuming bullets are tagged as "Player Weapon."
        {
            // Increment the hit count.
            hitCount++;

            // Perform any additional actions or effects on collision if needed.
            // For example, you can play an explosion animation or sound.

            // Check if the hit count has reached three, and if so, trigger the death animation.
            if (hitCount >= 3)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        // Perform any additional actions or effects before triggering the death animation.
        // For example, you can play an explosion animation or sound.

        // Trigger the death animation if an Animator is attached.
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Disable the Collider2D to stop collisions.
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // Stop movement by setting the Rigidbody2D velocity to zero.
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Disable the AI weapon system.
        if (aiWeaponSystem != null)
        {
            aiWeaponSystem.enabled = false;
        }

        // Play the death sound.
        if (audioSource != null && deathSound != null)
        {
            audioSource.Play();
        }

        // Delay the actual destruction to allow the death animation to play.
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Wait for some time to allow the death animation to finish playing.
        yield return new WaitForSeconds(1.0f); // Adjust the time as needed.

        // Destroy the enemy GameObject.
        Destroy(gameObject);
    }
}
