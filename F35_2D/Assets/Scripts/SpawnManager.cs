using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    //[SerializeField]
    //private GameObject _enemyContainer;
    private IEnumerator coroutine;
    private new Camera camera;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        coroutine = SpawnRoutine(5.0f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine(float waitTime)
    {
        

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = Vector3.zero;


            float randompos = Random.Range(0, 4);
            switch (randompos)
            {
                case 0: // Top Border
                    spawnPosition = new Vector3(Random.Range(camera.rect.xMin, camera.rect.xMax), camera.rect.yMax, 0);
                    break;

                case 1: // Right Border
                    spawnPosition = new Vector3(camera.rect.xMax, Random.Range(camera.rect.yMin, camera.rect.yMax), 0);
                    break;

                case 2: // Left Border
                    spawnPosition = new Vector3(camera.rect.xMin, Random.Range(camera.rect.yMin, camera.rect.yMax), 0);
                    break;
                case 3: // Bottom Border
                    spawnPosition = new Vector3(Random.Range(camera.rect.xMin, camera.rect.xMax), camera.rect.yMin, 0);
                    break;
            }

            spawnPosition = camera.ViewportToWorldPoint(spawnPosition);
            spawnPosition.z = 0; // Set the Z coordinate to 0.

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            //newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
