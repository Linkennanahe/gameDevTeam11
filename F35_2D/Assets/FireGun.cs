using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireGun : MonoBehaviour
{
    public Transform Gun;
    public GameObject BulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 0.2f; // Adjust this value to control the fire rate.
    public AudioClip bulletSound; // Assign the bullet firing sound in the Inspector.

    private float nextFireTime;
    
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireBullet()
    {
    GameObject bullet = Instantiate(BulletPrefab, Gun.position, Gun.rotation);
    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);

    // Add an AudioSource component to the bullet GameObject and play the sound
    AudioSource bulletAudioSource = bullet.AddComponent<AudioSource>();
    bulletAudioSource.clip = bulletSound;

    // Adjust the loudness (volume) of the sound (0.0f to 1.0f)
    bulletAudioSource.volume = 0.1f; 

    bulletAudioSource.Play();

    // Destroy the AudioSource component after the sound finishes playing
    Destroy(bulletAudioSource, bulletSound.length);
    }

}
