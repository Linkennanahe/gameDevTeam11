using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMissile : MonoBehaviour
{

    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float _rspeed = 200f;
    public Transform target;
    private Rigidbody2D rb;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * _rspeed;
        rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //    Player player = other.transform.GetComponent<Player>();
            //    if (player != null)
            //    {
            //        player.Damage();
            //    }

            Destroy(other.gameObject);
            Destroy(this.gameObject);

            _spawnManager.OnPlayerDeath();
        }
    }

}
