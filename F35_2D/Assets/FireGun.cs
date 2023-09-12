using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireGun : MonoBehaviour
{
    // Reference to the gun's transform.
    public Transform Gun;
    
    // Prefab for bullets and missiles.
    public GameObject BulletPrefab;
    public GameObject MissilePrefab;

    // Force applied to bullets and missiles.
    public float bulletForce = 20f;
    public float missileForce = 30f;

    // Rate of fire for bullets and missiles.
    public float bulletFireRate = 0.2f;
    public float missileFireRate = 0.5f;

    // Sounds for bullets and missiles.
    public AudioClip bulletSound;
    public AudioClip missileSound;

    // Array of weapon transforms.
    public Transform[] weapons;

    // Index of the current weapon.
    private int currentWeaponIndex = 0;
    
    // Maximum ammo counts for each weapon.
    public int maxBulletAmmo = 500; 
    public int maxMissileAmmo = 10;
    
    // Current ammo counts.
    private int currentBulletAmmo;
    private int currentMissileAmmo;

    // Firing interval
    private float nextBulletFireTime;
    private float nextMissileFireTime;

    void Start()
    {
        // Initialize the gun reference to the current weapon.
        Gun = weapons[currentWeaponIndex];

        // Initialize the current ammo counts.
        currentBulletAmmo = maxBulletAmmo;
        currentMissileAmmo = maxMissileAmmo;
    }

    void Update()
    {
        // Check if the space key is pressed (fire input).
        if (Keyboard.current.spaceKey.isPressed)
        {
            int currentAmmo = GetAmmoCount(currentWeaponIndex);

            // Check the current weapon index and fire based on the weapon type and fire rate.
            if (currentWeaponIndex == 0 && Time.time >= nextBulletFireTime && currentAmmo > 0)
            {
                FireBullet();
                nextBulletFireTime = Time.time + bulletFireRate;
                UpdateAmmoCount(currentWeaponIndex, currentAmmo - 1);
            }
            else if (currentWeaponIndex == 1 && Time.time >= nextMissileFireTime && currentAmmo > 0)
            {
                FireMissile();
                nextMissileFireTime = Time.time + missileFireRate;
                UpdateAmmoCount(currentWeaponIndex, currentAmmo - 1);
            }
        }

        // Output the current ammo counts for debugging.
        Debug.Log("Current Bullet Ammo: " + currentBulletAmmo);
        Debug.Log("Current Missile Ammo: " + currentMissileAmmo);

        // Check for weapon switch input (Q and E keys).
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {   
            // Switch to the previous weapon.
            SwitchWeapon(-1); 
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // Switch to the next weapon.
            SwitchWeapon(1); 
        }
    }

    // Function to fire a bullet.
    void FireBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab, Gun.position, Gun.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.up * bulletForce, ForceMode2D.Impulse);

        AudioSource bulletAudioSource = bullet.AddComponent<AudioSource>();
        bulletAudioSource.clip = bulletSound;
        bulletAudioSource.volume = 0.1f;
        bulletAudioSource.Play();
        Destroy(bulletAudioSource, bulletSound.length);
    }
    
    // Function to fire a missile.
    void FireMissile()
    {
        GameObject missile = Instantiate(MissilePrefab, Gun.position, Gun.rotation);
        Rigidbody2D rb = missile.GetComponent<Rigidbody2D>();
        rb.AddForce(Gun.up * missileForce, ForceMode2D.Impulse);

        AudioSource missileAudioSource = missile.AddComponent<AudioSource>();
        missileAudioSource.clip = missileSound;
        missileAudioSource.volume = 0.1f;
        missileAudioSource.Play();
        Destroy(missileAudioSource, missileSound.length);
    }

    // Function to switch weapons.
    void SwitchWeapon(int direction)
    {
        currentWeaponIndex += direction;

        // Ensure the weapon index stays within bounds.
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length - 1;
        }
        else if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        // Update the gun reference to the current weapon.
        Gun = weapons[currentWeaponIndex];
    }

    // Function to get the current ammo count for the selected weapon.
    int GetAmmoCount(int weaponIndex)
    {
        return weaponIndex == 0 ? currentBulletAmmo : currentMissileAmmo;
    }

    // Function to update the current ammo count for the selected weapon.
    void UpdateAmmoCount(int weaponIndex, int newAmmoCount)
    {
        if (weaponIndex == 0)
        {
            currentBulletAmmo = newAmmoCount;
        }
        else
        {
            currentMissileAmmo = newAmmoCount;
        }
    }

    // Function to add ammo with a random chance.
    public void AddAmmo()
    {
        // Define the probability (30% chance in this case).
        float probability = 0.3f;

        // Generate a random value between 0 and 1.
        float randomValue = Random.value;

        // Check if the random value is less than or equal to the probability.
        if (randomValue <= probability)
        {
            // Add 50 bullet ammo.
            currentBulletAmmo += 50;
        
            // Add 2 missile ammo.
            currentMissileAmmo += 2;

            // Ensure that the current ammo counts do not exceed their maximum values.
            currentBulletAmmo = Mathf.Clamp(currentBulletAmmo, 0, maxBulletAmmo);
            currentMissileAmmo = Mathf.Clamp(currentMissileAmmo, 0, maxMissileAmmo);
        }
    }
}
