using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[System.Serializable]
public class ScoreDataWrapper
{
    public List<ScoreData> scores = new List<ScoreData>();
}

public class ScoreHistoryController : MonoBehaviour
{
    private List<ScoreData> scoreHistory = new List<ScoreData>();

    private string SaveFilePath => Path.Combine(Application.persistentDataPath, "scoreHistory.json");

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None; // Hide by default
        root.Q<Button>("CloseBtn").clicked += () => LoadMainMenu();
        LoadScores();
        PopulateScoreList(root);
    }

    private void PopulateScoreList(VisualElement root)
    {
        var scoreList = root.Q<VisualElement>("ScoreList");
        if (scoreList == null)
        {
            Debug.LogError("❌ 'ScoreList' VisualElement not found in Scores.uxml!");
            return;
        }

        scoreList.Clear();

        for (int i = 0; i < scoreHistory.Count; i++)
        {
            var entry = scoreHistory[i];
            int rank = i + 1;

            // Row container
            var row = new VisualElement();
            row.style.flexDirection = FlexDirection.Row;
            row.style.justifyContent = Justify.SpaceBetween;
            row.style.paddingTop = 8;
            row.style.paddingBottom = 8;
            row.style.paddingLeft = 5;
            row.style.paddingRight = 5;
            row.style.borderBottomWidth = 1;
            row.style.borderBottomColor = new StyleColor(new UnityEngine.Color(1f, 1f, 1f, 0.1f));

            // Rank + Name label
            var nameLabel = new Label($"{rank}. {entry.playerName}");
            nameLabel.style.color = new StyleColor(UnityEngine.Color.white);
            nameLabel.style.fontSize = 16;

            // Score label
            var scoreLabel = new Label(entry.score.ToString());
            scoreLabel.style.color = new StyleColor(new UnityEngine.Color(0.4f, 0.9f, 0.4f));
            scoreLabel.style.fontSize = 16;

            row.Add(nameLabel);
            row.Add(scoreLabel);
            scoreList.Add(row);
        }
    }

    private void LoadMainMenu()
    {
        print ("Returning to Main Menu...");
 
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_Scene");
    }

    /// <summary>
    /// Adds a score, sorts history, keeps only top 10, and saves to file.
    /// </summary>
    public void AddScore(string playerName, int score)
    {
        // Add new score
        scoreHistory.Add(new ScoreData { playerName = playerName, score = score });

        // Sort descending by score and keep top 10
        scoreHistory = scoreHistory.OrderByDescending(s => s.score).Take(10).ToList();

        SaveScores();

        // Refresh the displayed list
        var root = GetComponent<UIDocument>().rootVisualElement;
        PopulateScoreList(root);
    }

    private void SaveScores()
    {
        ScoreDataWrapper wrapper = new ScoreDataWrapper { scores = scoreHistory };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(SaveFilePath, json);
    }

    private void LoadScores()
    {
        if (File.Exists(SaveFilePath))
        {   print("Loading score history from: " + SaveFilePath);
            string json = File.ReadAllText(SaveFilePath);
            ScoreDataWrapper wrapper = JsonUtility.FromJson<ScoreDataWrapper>(json);
            if (wrapper != null && wrapper.scores != null)
            {
                scoreHistory = wrapper.scores;
            }
        }
        else
        {
            print("No existing score history found. Loading dummy scores.");

            // Dummy scores shown on first launch
            scoreHistory = new List<ScoreData>
            {
                new ScoreData { playerName = "Ace",    score = 5000 },
                new ScoreData { playerName = "Shadow", score = 4500 },
                new ScoreData { playerName = "Luna",   score = 4000 },
                new ScoreData { playerName = "Blaze",  score = 3500 },
                new ScoreData { playerName = "Storm",  score = 3000 },
                new ScoreData { playerName = "Nova",   score = 2500 },
                new ScoreData { playerName = "Pixel",  score = 2000 },
                new ScoreData { playerName = "Echo",   score = 1500 },
                new ScoreData { playerName = "Frost",  score = 1000 },
                new ScoreData { playerName = "Ghost",  score = 500  },
            };

            // Save so dummy scores persist for next launch
            SaveScores();
        }
    }

}
