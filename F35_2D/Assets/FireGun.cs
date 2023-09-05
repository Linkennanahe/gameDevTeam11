using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireGun : MonoBehaviour
{
    public Transform Gun;
    public GameObject Bullet;
    public float bulletForce = 20f;
    public float fireRate = 0.5f; // Adjust this value to control the fire rate.

    private float nextFireTime; // Tracks the time when the next shot can be fired.

    void Update()
    {
        // Check if enough time has passed since the last shot to allow firing again.
        if (Keyboard.current.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            FireBullet();
            // Set the nextFireTime to the current time plus the fire rate.
            nextFireTime = Time.time + fireRate;
        }  
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);
    }
}
