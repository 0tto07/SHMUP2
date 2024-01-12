using UnityEngine;

public class ShipProjectile : MonoBehaviour

{
    
    public GameObject NoSpawnPrefab = null;
    public bool IsSpawner = false;
    public float speed = 10f;
    public Vector2 direction = Vector2.right;

    void Start()
    {
        // Spawn projectiles at different angles
        SpawnProjectile(Vector2.right);   // Forward
        SpawnProjectile(Quaternion.Euler(0, 0, 30) * Vector2.right);  // 30 degrees
        SpawnProjectile(Quaternion.Euler(0, 0, -30) * Vector2.right); // -30 degrees
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void SpawnProjectile(Vector2 newDirection)
    {
        if (gameObject.activeInHierarchy && IsSpawner) // Check if the original object is still active
        {
            ShipProjectile newProjectile = Instantiate(NoSpawnPrefab.GetComponent<ShipProjectile>(), this.transform.position, Quaternion.identity);
            newProjectile.direction = newDirection;
            newProjectile.gameObject.SetActive(true); // Activate the new projectile
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyProjectile")) // Check if the collider is an enemy projectile
        {
            Destroy(other.gameObject); // Destroy the enemy projectile
            Destroy(gameObject); // Destroy this projectile
        }
        if (other.CompareTag("Enemy")) // Check if the collider is an enemy projectile
        {
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy this projectile
        }

    }


     public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}
