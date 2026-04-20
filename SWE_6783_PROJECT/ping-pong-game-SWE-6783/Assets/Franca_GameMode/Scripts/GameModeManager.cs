using UnityEngine;
using TMPro;
using System;
using System.Collections;

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

    //public BallController ball;
    public GameObject ballPrefab;
    public GameObject playerObj;
    public GameObject aiObj;

    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;
    public TMP_Text timerText;
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public TMP_Text winnerText;
    public int winningValue = 5;
    private string restartText = "\nPress escape to open the menu";
    public float startTime = 5f;
    public TMP_Text gameStartText;



    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                EndMatch("Time has run out!" + restartText);
            }
        }
    }
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        int getLevel = PlayerPrefs.GetInt("Difficulty", 0);
        DifficultyData difficulty = levelSettings[getLevel];
        ApplyDifficulty(difficulty);
        timerIsRunning = false;
        winnerText.gameObject.SetActive(false);
        ballPrefab.SetActive(false);
        playerObj.SetActive(false);
        aiObj.SetActive(false);
        StartCoroutine(CountdownToStart());
    }
    void ApplyDifficulty(DifficultyData difficulty)
    {
        playerObj.GetComponent<PlayerControls>().speed = difficulty.playerSpeed;
        playerObj.transform.localScale = new Vector3(1, difficulty.playerYScale, 1);
        aiObj.GetComponent<OpponentScript>().speed = difficulty.aiSpeed;
        aiObj.transform.localScale = new Vector3(1, difficulty.aiYScale, 1);

        ballPrefab.GetComponent<BallControl>().speed = difficulty.ballSpeed;
    }

    IEnumerator CountdownToStart()
    {
        float timeLeft = startTime;
        while (timeLeft > 0)
        {
            gameStartText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
        gameStartText.text = "GO!";
        yield return new WaitForSeconds(0.5f);
        gameStartText.gameObject.SetActive(false);
        timerIsRunning = true;
        ballPrefab.SetActive(true);
        playerObj.SetActive(true);
        aiObj.SetActive(true);
        yield return null; // wait one frame so BallControl.Start() runs and assigns rb2d
        ResetBall();
    }

    public void GoalScored(string wall)
    {
        if (wall == "LeftWall")
            rightScore++;
        else
            leftScore++;

        Debug.Log(rightScore + " Right Score");
        Debug.Log(leftScore + " Left Score");
        if (leftScore >= winningValue)
        {
            timerIsRunning = false;
            EndMatch("Player 1 Wins!" + restartText);
        } else if(rightScore >= winningValue)
        {
            timerIsRunning = false;
            EndMatch("Player 2 Wins!" + restartText);
        }

        UpdateUI();
        ResetBall();
    }

    void EndMatch(string winnerTextVal)
    {
        Time.timeScale = 0;
        Debug.Log("Match Over!");
        winnerText.text = winnerTextVal;
        winnerText.gameObject.SetActive(true);
    }

    void UpdateUI()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    void ResetBall()
    {
        ballPrefab.transform.position = Vector3.zero;
        ballPrefab.GetComponent<BallControl>().Launch();
    }
    void DisplayTime(float timeToDisplay)
    {
        // Formats the float into Minutes:Seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
