using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;

    void Start()
    {
        if (playerData == null)
        {
            Debug.LogError("PlayerData reference is missing in GameManager.");
        }
        else
        {
            playerData.Points = 0;
        }
    }

    // This method is automatically called by Unity when this GameObject collides with another 2D collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy or an enemy projectile
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            // Decrease the player's HP
            playerData.DecreaseHP();
        }
    }
}
