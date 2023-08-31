using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    //Enemy fields
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private float _rspeed = 100f;
    public Transform target;
    private Rigidbody2D rb;

    //Enemy Missile fields
    [SerializeField]
    private GameObject _enemyMissilePrefab;
    //[SerializeField]
    //private GameObject _enemyContainer;
    [SerializeField]
    private float _spawnInterval = 20.0f;
    private float _nextSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyBehaviour();
        if(Time.time >= _nextSpawnTime)
        {
            EnemyMissileSpawner();
            _nextSpawnTime = Time.time + _spawnInterval;
        }
        

    }
    void EnemyBehaviour()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * _rspeed;
        rb.velocity = transform.up * _speed;
    }

    void EnemyMissileSpawner()
    {
        GameObject newEnemy = Instantiate(_enemyMissilePrefab, transform.position, Quaternion.identity);

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.tag == "Laser") // REPLACE THIS WITH THE PLAYER'S BULLET TAG
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }
    
}
