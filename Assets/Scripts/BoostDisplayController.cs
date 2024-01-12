using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BoostDisplayController : MonoBehaviour
{
    public Ship ship; // Reference to the Ship script
    public SpriteRenderer boostDisplayImage; // Reference to the Image component for displaying boost
    public Sprite[] boostLevelSprites; // Array of sprites for different boost levels

    void Update()
    {
        if (ship != null && boostDisplayImage != null && boostLevelSprites.Length >5)
        {
            // Calculate the boost level index based on the current boost amount
            int boostLevelIndex = Mathf.FloorToInt((ship.boostAmount / ship.maxBoost) * 5);

            // Update the display image based on the boost level
            boostDisplayImage.sprite = boostLevelSprites[boostLevelIndex];
        }
    }
}