using UnityEngine;
using TMPro;
public class ShowDifficulty : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextMeshProUGUI myText = GetComponent<TextMeshProUGUI>();

        // 2. Change the 'text' property
        myText.text = SetDifficulty(PlayerPrefs.GetInt("Difficulty", 0));
    }


    private string SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                return "Easy";
            case 1:
                return "Medium";
            case 2:
                return "Hard";
            default:
                return "Unknown";
        }
    }

}
