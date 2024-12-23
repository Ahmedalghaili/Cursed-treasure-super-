using UnityEngine;
using TMPro; // Required for TextMeshPro

public class EnhancedTimer : MonoBehaviour
{
    // Reference to the TextMeshPro object
    public TextMeshProUGUI timerText;

    // Timer variables
    private float startTime;
    private bool isTimerRunning = false;

    // Countdown mode variables
    public bool isCountdown = false; // Enable countdown mode
    public float countdownTime = 60f; // Set countdown time (in seconds)

    // Text effects
    public Color normalColor = Color.green;
    public Color warningColor = Color.yellow;
    public Color dangerColor = Color.red;

    void Start()
    {
        // Check if timerText is assigned in the Inspector
        if (timerText == null)
        {
            Debug.LogError("Timer Text is not assigned in the Inspector!");
            return;
        }

        // Optionally, make sure the timerText survives across scenes (if desired)
        // DontDestroyOnLoad(timerText.gameObject);

        // Initialize timer
        startTime = Time.time;
        isTimerRunning = true;

        // Set initial text properties
        timerText.color = normalColor; // Default color
        timerText.fontSize = 36; // Default font size
    }

    void Update()
    {
        if (timerText == null) // Exit if timerText is destroyed or null
        {
            Debug.LogWarning("timerText has been destroyed or is null!");
            return;
        }

        if (!isTimerRunning) return;

        float currentTime;
        if (isCountdown)
        {
            // Countdown mode
            currentTime = countdownTime - (Time.time - startTime);

            if (currentTime <= 0)
            {
                // Stop the timer when countdown reaches 0
                isTimerRunning = false;
                timerText.text = "Time's Up!";
                timerText.color = dangerColor; // Final color
                timerText.fontSize = 50; // Make it larger
                return;
            }
        }
        else
        {
            // Stopwatch mode
            currentTime = Time.time - startTime;
        }

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Update the timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Apply effects based on time thresholds
        ApplyVisualEffects(currentTime);
        AnimateText();
    }

    void ApplyVisualEffects(float currentTime)
    {
        if (timerText == null) return; // Exit if timerText is destroyed or null

        if (isCountdown)
        {
            // In countdown mode, change color based on remaining time
            if (currentTime <= 10) // Danger zone
            {
                timerText.color = dangerColor;
            }
            else if (currentTime <= 30) // Warning zone
            {
                timerText.color = warningColor;
            }
            else
            {
                timerText.color = normalColor; // Safe zone
            }
        }
        else
        {
            // In stopwatch mode, change color dynamically (optional)
            if (currentTime >= 50)
            {
                timerText.color = dangerColor; // After 50 seconds
            }
            else if (currentTime >= 30)
            {
                timerText.color = warningColor; // After 30 seconds
            }
            else
            {
                timerText.color = normalColor; // Default
            }
        }
    }

    void AnimateText()
    {
        if (timerText == null) return; // Exit if timerText is destroyed or null

        // Create a pulsating effect by scaling the text up and down
        float pulse = Mathf.PingPong(Time.time, 1) * 0.05f; // Pulsate between 0 and 0.05
        timerText.rectTransform.localScale = new Vector3(1 + pulse, 1 + pulse, 1);
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        if (timerText == null) return; // Exit if timerText is destroyed or null

        startTime = Time.time;
        isTimerRunning = true;
        timerText.color = normalColor; // Reset color
    }
}
