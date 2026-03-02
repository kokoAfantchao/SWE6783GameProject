using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreHistoryController : MonoBehaviour
{
    public VisualTreeAsset scoreEntryTemplate; // Assign ScoreEntry.uxml here
    private ListView scoreListView;
    private List<ScoreData> scoreHistory = new List<ScoreData>();

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None; // Hide by default
        scoreListView = root.Q<ListView>("ScoreList");

        // Mock Data - You would normally load this from a Save System
        scoreHistory.Add(new ScoreData { playerName = "Ace", score = 2500 });
        scoreHistory.Add(new ScoreData { playerName = "Shadow", score = 1800 });
        scoreHistory.Add(new ScoreData { playerName = "Luna", score = 1200 });

        SetupListView();
    }

    void SetupListView()
    {
        // 1. Tell the list where the data comes from
        scoreListView.itemsSource = scoreHistory;

        // 2. Tell the list how to create a new row (using your template)
        scoreListView.makeItem = () => scoreEntryTemplate.Instantiate();

        // 3. Tell the list how to fill a row with data
        scoreListView.bindItem = (VisualElement element, int index) =>
        {
            var data = scoreHistory[index];
            element.Q<Label>("PlayerName").text = data.playerName;
            element.Q<Label>("ScoreValue").text = data.score.ToString();
        };
    }
}
