using UnityEngine;
using TMPro;

public class MatchTimer : MonoBehaviour
{
    public float matchTime = 120f; // 2 minutes
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;

    public GameModeManager gameMode; // reference to your score manager
    public Rigidbody2D ballRb;
    public Rigidbody2D leftPaddleRb;
    public Rigidbody2D rightPaddleRb;

    public bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        matchTime -= Time.deltaTime;

        if (matchTime < 0)
        {
            matchTime = 0;
            isRunning = false;
            EndMatch();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(matchTime / 60);
        int seconds = Mathf.FloorToInt(matchTime % 60);

        timerText.text = $"{minutes:0}:{seconds:00}";
    }

    void EndMatch()
    {
        Debug.Log("Match Over!");

        // 1. Freeze ball
        ballRb.linearVelocity = Vector2.zero;
        ballRb.simulated = false;

        // 2. Freeze paddles
        leftPaddleRb.simulated = false;
        rightPaddleRb.simulated = false;

        // 3. Determine winner
        int leftScore = gameMode.leftScore;
        int rightScore = gameMode.rightScore;

        if (leftScore > rightScore)
            winnerText.text = "Player 1 Wins!";
        else if (rightScore > leftScore)
            winnerText.text = "Player 2 (AI) Wins!";
        else
            winnerText.text = "Draw!";

        winnerText.gameObject.SetActive(true);
    }
}
