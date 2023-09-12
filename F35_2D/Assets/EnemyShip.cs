using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    // Maximum health of the enemy.
    public int maxHealth = 10;

    // Damage taken from player weapons.
    public int playerWeaponDamage = 1;

    // Damage taken when colliding with another enemy.
    public int enemyCollisionDamage = 3;

    // Damage taken when colliding with the player.
    public int playerCollisionDamage = 3;

    // Damage taken from enemy bullets.
    public int enemyBulletDamage = 1;

    // Damage taken from missiles.
    public int missileDamage = 10;

    // Current health of the enemy.
    private int currentHealth;

    // Reference to the Animator component.
    private Animator animator;

    // Reference to the Collider2D component.
    private Collider2D enemyCollider;

    // Reference to the Rigidbody2D component.
    private Rigidbody2D rb;

    // Reference to the AI weapon system script.
    private AIWeaponSystem aiWeaponSystem;

    // Reference to the AudioSource component for death sound.
    private AudioSource audioSource;

    // Assign the death sound in the Inspector.
    public AudioClip deathSound;

    // Reference to the player's FireGun script.
    private FireGun playerFireGun;

    private void Start()
    {
        // Initialize the current health to the maximum health.
        currentHealth = maxHealth;

        // Get the Animator component.
        animator = GetComponent<Animator>();

        // Get the Collider2D component.
        enemyCollider = GetComponent<Collider2D>();

        // Get the Rigidbody2D component.
        rb = GetComponent<Rigidbody2D>();

        // Get the AI weapon system script.
        aiWeaponSystem = GetComponent<AIWeaponSystem>();

        // Get the AudioSource component.
        audioSource = GetComponent<AudioSource>();

        // Set the death sound for the AudioSource component.
        audioSource.clip = deathSound;

        // Find the player GameObject with the "Player" tag.
        GameObject playerGameObject = GameObject.FindGameObjectWithTag("Player");

        // If the player GameObject is found, get the FireGun script attached to it.
        if (playerGameObject != null)
        {
            playerFireGun = playerGameObject.GetComponent<FireGun>();
        }
        else
        {
            Debug.LogWarning("Player GameObject not found.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check the collision tag and apply damage accordingly.
        if (collision.gameObject.CompareTag("Player Weapon"))
        {
            // Decrement the current health by playerWeaponDamage.
            currentHealth -= playerWeaponDamage;
        }
        else if (collision.gameObject.CompareTag("Player Missile"))
        {
            // Decrement the current health by missileDamage.
            currentHealth -= missileDamage;
        }
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            // Ignore collisions with objects tagged as "Enemy" and "Player."
            // You can leave this block empty if you want to completely ignore the collision.
            // Alternatively, you can add custom logic here if needed.
        }
        else if (collision.gameObject.CompareTag("Enemy Weapon"))
        {
            // Decrement the current health by enemyBulletDamage.
            currentHealth -= enemyBulletDamage;
        }

        // Check if the enemy depletes their health, if yes, trigger the death animation.
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
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

        // Calls the AddAmmo function in FireGun script.
        if (playerFireGun != null)
        {
            playerFireGun.AddAmmo();
        }

        // Delay the actual destruction to allow the death animation to finish playing.
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
