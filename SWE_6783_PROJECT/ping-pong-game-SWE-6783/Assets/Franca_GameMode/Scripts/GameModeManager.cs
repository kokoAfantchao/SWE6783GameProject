using UnityEngine;
using TMPro;

public class GameModeManager : MonoBehaviour
{
    public DifficultyData[] levelSettings;
    public enum Player
    {
        Player1,
        Player2
    }

    public static GameModeManager Instance;

    public int leftScore = 0;
    public int rightScore = 0;

    public BallController ball;
    public GameObject ballPrefab;
    public GameObject playerObj;
    public GameObject aiObj;

    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        int getLevel = PlayerPrefs.GetInt("Difficulty", 0);
        DifficultyData difficulty = levelSettings[getLevel];
        ApplyDifficulty(difficulty);
    }
    void ApplyDifficulty(DifficultyData difficulty)
    {
        playerObj.GetComponent<PlayerControls>().speed = difficulty.playerSpeed;
        playerObj.transform.localScale = new Vector3(1, difficulty.playerYScale, 1);
        aiObj.GetComponent<OpponentScript>().speed = difficulty.playerSpeed;
        aiObj.transform.localScale = new Vector3(1, difficulty.aiYScale, 1);

        ballPrefab.GetComponent<BallControl>().speed = difficulty.ballSpeed;
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
