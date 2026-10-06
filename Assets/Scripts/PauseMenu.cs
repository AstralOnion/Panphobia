using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("HUD")]
    [SerializeField] private TMP_Text hitsText;

    [Header("Win Condition")]
    [SerializeField] private float timeToWin = 60f;

    [Header("Lose Condition")]
    [SerializeField] private int hitsToLose = 3;

    [Header("Hit Settings")]
    [SerializeField] private float hitCooldown = 1f;

    private bool isPaused;
    private bool gameOver;

    private float gameTimer;
    private int hitCount;

    private float lastHitTime = -Mathf.Infinity;


    void Start()
    {
        // Make sure the game starts unpaused.
        Time.timeScale = 1f;

        // Hide all panels.
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        isPaused = false;
        gameOver = false;

        gameTimer = 0f;
        hitCount = 0;

        lastHitTime = -Mathf.Infinity;

        // Update the HUD.
        UpdateHitText();
    }


    void Update()
    {
        // Don't count time while paused or after the game ends.
        if (isPaused || gameOver)
            return;

        gameTimer += Time.deltaTime;

        if (gameTimer >= timeToWin)
        {
            Win();
        }
    }


    // =========================
    // HIT COUNTER
    // =========================

    private void UpdateHitText()
    {
        int hitsLeft = hitsToLose - hitCount;

        hitsText.text = "Hits Left: " + hitsLeft;
    }


    public void RegisterHit()
    {
        if (gameOver)
            return;

        // Prevent hits from happening too quickly.
        if (Time.time < lastHitTime + hitCooldown)
            return;

        lastHitTime = Time.time;

        hitCount++;

        UpdateHitText();

        Debug.Log("Player hit! " + hitCount + "/" + hitsToLose);

        if (hitCount >= hitsToLose)
        {
            Lose();
        }
    }


    // =========================
    // PAUSE
    // =========================

    public void Pause()
    {
        if (gameOver)
            return;

        isPaused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }


    public void Resume()
    {
        if (gameOver)
            return;

        isPaused = false;

        pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }


    // =========================
    // WIN
    // =========================

    public void Win()
    {
        if (gameOver)
            return;

        gameOver = true;
        isPaused = false;

        pausePanel.SetActive(false);
        winPanel.SetActive(true);
        losePanel.SetActive(false);

        Time.timeScale = 0f;

        Debug.Log("Player Wins!");
    }


    // =========================
    // LOSE
    // =========================

    public void Lose()
    {
        if (gameOver)
            return;

        gameOver = true;
        isPaused = false;

        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("Player Loses!");
    }


    // =========================
    // RESTART
    // =========================

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // =========================
    // MAIN MENU
    // =========================

    public void MainMenu(string sceneName)
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneName);
    }
}
