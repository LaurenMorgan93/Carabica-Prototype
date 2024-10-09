using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour

// this script handles the player's hitpoints, and their score (distance travelled)
{

    public ScreenShake shakeScript;
    public float DistanceTravelled { get; private set; } = 0f; 

    // The speed that the distance counter increases
    public float SpeedMultiplier { get; set; } = 1f;
    
    public TextMeshProUGUI DistanceDisplayText; // The display text for distance travelled

    // Time interval in seconds to increase distance travelled (every second)
    private float distanceUpdateInterval = 1f;
    private float elapsedDistanceUpdateTime = 0f;

    // Health / Hit tracking
    public int MaxHitPoints = 3; // Changeable in the inspector
    public int CurrentHitPoints;

    public TextMeshProUGUI HealthDisplayText;
    public Sprite[] healthIcons;
    public Sprite[] dashBoardStatusIcons;
    public SpriteRenderer healthReflectionRend;
    public SpriteRenderer dashBoardRend;

    // Game over flag
    private bool IsGameOver = false;

    void Start()
    {
        // start with max hitpoints
        CurrentHitPoints = MaxHitPoints;
    }

    void Update()
    {
        // Update elapsed time only if the game is not over
        if (IsGameOver) return;

        elapsedDistanceUpdateTime += Time.deltaTime;

        // Check if the time interval has passed
        if (elapsedDistanceUpdateTime >= distanceUpdateInterval)
        {
            // Increase the distance travelled by 0.1 * speed multiplier
            DistanceTravelled += 0.1f * SpeedMultiplier;

            // Round to the nearest 0.1
            DistanceTravelled = Mathf.Round(DistanceTravelled * 10f) / 10f;

            // Update the display text
            DistanceDisplayText.text = DistanceTravelled + " KMs";

            // Reset elapsed time
            elapsedDistanceUpdateTime = 0f;
        }

        HealthDisplayText.text = "Health Points Left: " + CurrentHitPoints;

        healthReflectionRend.sprite = healthIcons[CurrentHitPoints - 1];
        dashBoardRend.sprite = dashBoardStatusIcons[CurrentHitPoints - 1];
    }

    public void SaveScore()
    {
        // Save the DistanceTravelled to PlayerPrefs
        PlayerPrefs.SetFloat("DistanceTravelled", DistanceTravelled);
    }

    // Call this method to take damage
    public void TakeDamage()
    {
        if (IsGameOver) return;

        shakeScript.TriggerShake(shakeScript.shakeTime, shakeScript.shakeStrength); // on collision shake the screen 
        // Decrease current hit points
        CurrentHitPoints--;

        // Check if the current hit points reach zero
        if (CurrentHitPoints <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        print("aaaa");

        // Set the game over flag
        IsGameOver = true;
        SaveScore(); // save the distance travelled to pps
        // load the Game Over scene
        SceneManager.LoadScene("GameOver");

    }
}
