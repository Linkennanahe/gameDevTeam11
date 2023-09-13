using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab you want to spawn.
    public float spawnInterval = 3.0f; // Time interval between enemy spawns.
    public float minSpawnDistance = 10.0f; // Minimum distance from the player to spawn enemies.
    public float maxSpawnDistance = 15.0f; // Maximum distance from the player to spawn enemies.

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
            // Calculate a random spawn distance between minSpawnDistance and maxSpawnDistance.
            float randomSpawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

            // Calculate a random spawn angle between 0 and 360 degrees.
            float randomSpawnAngle = Random.Range(0f, 360f);

            // Convert the angle to radians.
            float randomSpawnAngleRad = randomSpawnAngle * Mathf.Deg2Rad;

            // Calculate the spawn position based on the player's position, random distance, and angle in 2D space.
            Vector3 spawnPosition = player.position + new Vector3(Mathf.Cos(randomSpawnAngleRad), Mathf.Sin(randomSpawnAngleRad), 0f) * randomSpawnDistance;

            // Instantiate an enemy prefab at the calculated spawn position.
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Wait for the specified interval before spawning the next enemy.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
