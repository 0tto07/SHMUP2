using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float fireRate = 0.5f; // Time between shots
    private float nextFireTime = 0f; // Time until next shot
    public float spawnDistance = 1f; // Distance from the origin to spawn the projectile

    void Update()
    {
        // Check if the current time is greater than the next fire time
        if (Time.time > nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate; // Schedule the next shot
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            Vector3 spawnPosition = transform.position + transform.right * spawnDistance;
            Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
