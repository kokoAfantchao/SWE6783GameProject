using UnityEngine;
using TMPro;

public class GameModeManager : MonoBehaviour
{
    public enum Player
    {
        Player1,
        Player2
    }

    public static GameModeManager Instance;

    public int leftScore = 0;
    public int rightScore = 0;

    public BallController ball;

    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    void Awake()
    {
        Instance = this;
    }

    public void GoalScored(bool leftGoal)
    {
        if (leftGoal)
            rightScore++;
        else
            leftScore++;

        UpdateUI();
        ResetBall();
    }

    void UpdateUI()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    void ResetBall()
    {
        ball.transform.position = Vector3.zero;
        ball.Launch();
    }
}
