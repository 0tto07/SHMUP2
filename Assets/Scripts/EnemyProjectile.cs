using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;
    public Vector2 direction = Vector2.right;

    public Rigidbody2D rb;
    public PlayerData PlayerData;
    private bool hpDecreased = false; // Flag to ensure HP is decreased only once


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }


    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ShipProjectile"))
        {
            Destroy(collision.gameObject); // Destroy the collided object
            Destroy(gameObject); // Destroy this enemy projectile

        }

        if (collision.gameObject.CompareTag("Ship"))
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            if (ship != null)
            {
                ship.DecreaseHP(); // Decrease the ship's HP
            }
        }
    }
}