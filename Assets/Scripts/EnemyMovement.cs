using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float verticalSpeed = 3f;
    public float minY = -5f;
    public float maxY = 5f;

    private float movementDirection = 1f;
    private float changeDirectionTime = 2f;
    private float timer = 0f;

    public PlayerData playerData;
    private bool hpDecreased = false; // Flag to ensure HP is decreased only once

    void Update()
    {
        // Move the enemy horizontally and vertically
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        transform.Translate(Vector2.up * verticalSpeed * movementDirection * Time.deltaTime);

        // Reverse direction if outside vertical bounds
        if (transform.position.y > maxY || transform.position.y < minY)
        {
            movementDirection = -movementDirection;
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY));
        }

        // Change vertical direction periodically
        timer += Time.deltaTime;
        if (timer > changeDirectionTime)
        {
            timer = 0f;
            movementDirection = -movementDirection;
            changeDirectionTime = Random.Range(1f, 4f);
        }

        // Check if the enemy has crossed the player's x-position to decrease HP
        if (transform.position.x < 0 && !hpDecreased)
        {
            if (playerData != null)
            {
                playerData.DecreaseHP();
                hpDecreased = true; // Set flag to true after decreasing HP
            }
        }

        // Destroy the enemy when it goes beyond the left edge of the screen
        if (transform.position.x < 0) // Adjust this value as needed
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            if (ship != null)
            {
                ship.DecreaseHP(); // Decrease the ship's HP
            }
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
