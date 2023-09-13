using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponSystem : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletForce = 20f;
    public float fireRate = 1f; // Adjust this value to control the AI's fire rate.
    public float firingDistance = 5f; // The distance at which the AI will start firing.

    private Transform playerTransform;
    private float timeSinceLastFire = 0f;

    private void Start()
    {
        // Find the player's Transform (you can also assign this reference in the Inspector).
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the distance to the player.
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Check if the AI is within firing distance, and enough time has passed since the last shot.
        if (distanceToPlayer <= firingDistance && Time.time - timeSinceLastFire > 1f / fireRate)
        {
            // Calculate the direction to the player.
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize();

            // Calculate the angle between the AI's forward direction (transform.up) and the direction to the player.
            float angle = Vector3.Angle(transform.up, directionToPlayer);

            // Check if the AI is facing the player.
            if (angle < 20f)
            {
                FireBullet();
                timeSinceLastFire = Time.time;
            }
        }
    }

    private void FireBullet()
    {
        // Instantiate a bullet at the bullet spawn point.
        GameObject EnemyBullet = Instantiate(EnemyBulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Apply force to the bullet to make it move forward.
        Rigidbody2D rb = EnemyBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawnPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
