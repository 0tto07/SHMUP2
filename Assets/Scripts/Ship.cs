using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    public Rigidbody2D myRigidBody = null;

    [Header("Player Data")]
    public PlayerData currentPlayerData;

    [Header("UI Elements")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI pointText;
    public Slider boostBarSlider;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float boostMultiplier = 2f;

    [Header("Boost Settings")]
    public float boostAmount = 100f;
    public float maxBoost = 100f;
    public float boostDepletionRate = 30f;
    public float boostRechargeRate = 5f;
    public bool canBoost = true;

    [Header("Shooting Settings")]
    public GameObject projectilePrefab;
    public float fireRate = 0.5f;
    public float nextFireTime = 0f;

    public bool shouldMoveUp;
    public bool shouldMoveDown;
    public bool shouldMoveLeft;
    public bool shouldMoveRight;
    public bool isBoosting;

    void Start()
    {
        currentPlayerData.HP = 3;
        InitializeBoostBar();
    }

    void Update()
    {
        UpdatePlayerUI();
        HandleMovementInput();
        HandleBoost();
        HandleShooting();

        CheckPlayerHP();
    }

    void FixedUpdate()
    {
        MoveShip();
    }

    public void InitializeBoostBar()
    {
        if (boostBarSlider != null)
        {
            boostBarSlider.maxValue = maxBoost;
            boostBarSlider.value = boostAmount;
        }
    }

    public void UpdatePlayerUI()
    {
        if (currentPlayerData != null)
        {
            hpText.text = $"{currentPlayerData.HP} HP";
            pointText.text = $"{currentPlayerData.Points} Points";
        }
        else
        {
            Debug.LogError("currentPlayerData is null.");
        }
    }

    public void HandleMovementInput()
    {
        shouldMoveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        shouldMoveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        shouldMoveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        shouldMoveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    public void HandleBoost()
    {
        isBoosting = canBoost && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && boostAmount > 0;

        if (isBoosting)
            DepleteBoost();
        else
            RechargeBoost();

        UpdateBoostBar();
    }

    public void DepleteBoost()
    {
        boostAmount -= boostDepletionRate * Time.deltaTime;
        if (boostAmount <= 0)
        {
            boostAmount = 0;
            canBoost = false;
        }
    }

    public void RechargeBoost()
    {
        if (boostAmount < maxBoost)
        {
            boostAmount += boostRechargeRate * Time.deltaTime;
            if (boostAmount >= maxBoost * 0.2f)
                canBoost = true;
        }
    }

    public void UpdateBoostBar()
    {
        if (boostBarSlider != null)
            boostBarSlider.value = boostAmount;
    }

    public void HandleShooting()
    {
        if (Time.time > nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void MoveShip()
    {
        Vector2 position = myRigidBody.position;
        float speed = isBoosting ? moveSpeed * boostMultiplier : moveSpeed;
        Vector2 movement = new Vector2(shouldMoveRight.CompareTo(shouldMoveLeft), shouldMoveUp.CompareTo(shouldMoveDown)) * speed * Time.deltaTime;

        if (movement.magnitude > 1)
            movement.Normalize();

        position += movement;
        position.x = Mathf.Clamp(position.x, 1f, 16.75f);
        position.y = Mathf.Clamp(position.y, 0.3f, 9.6f);

        myRigidBody.position = position;
    }

    public void FireProjectile()
    {
        Vector3 spawnPosition = transform.position;
        ShipProjectile newProjectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<ShipProjectile>();
        newProjectile.SetDirection(Vector2.right);
    }

    private void CheckPlayerHP()
    {
        if (currentPlayerData != null && currentPlayerData.HP <= 0)
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Method to decrease the ship's HP
    public void DecreaseHP()
    {
        if (currentPlayerData != null)
        {
            currentPlayerData.HP -= 1; // Decrease HP by 1

            // Update UI
            UpdatePlayerUI();

            // Check if HP is depleted and restart the game if necessary
            if (currentPlayerData.HP <= 0)
            {
                RestartGame();
                currentPlayerData.HP = 3; // Reset HP for the new game
            }
        }
        else
        {
            Debug.LogError("currentPlayerData is null.");
        }
    }
}
