using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab you want to spawn.
    public float spawnInterval = 3.0f; // Time interval between enemy spawns.
    public float spawnDistance = 10.0f; // Distance from the player to spawn enemies.

    private Transform player; // Reference to the player's transform.

    private void Start()
    {
        // Find the player GameObject and get its transform.
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Start spawning enemies at intervals.
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true) // This loop will keep running indefinitely.
        {
            // Calculate the spawn position based on the player's position and a fixed spawn distance in 2D space.
            Vector3 spawnPosition = player.position + player.up * spawnDistance;

            // Instantiate an enemy prefab at the calculated spawn position.
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Wait for the specified interval before spawning the next enemy.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
