using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    private VisualElement root;
    private bool isPaused = false;
    private ScoreHistoryController scoreHistoryController;
    private VisualElement scoreHistoryRoot;

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        // Hide the menu by default at start
        root.style.display = DisplayStyle.None;

        root.style.position = Position.Absolute;
        root.style.width = Length.Percent(100);
        root.style.height = Length.Percent(100);

        scoreHistoryController = FindObjectOfType<ScoreHistoryController>();
        scoreHistoryRoot = scoreHistoryController

        .GetComponent<UIDocument>()
        .rootVisualElement;

        // Assign Button Events
        root.Q<Button>("ResumeBtn").clicked += () => TogglePause();
        root.Q<Button>("RestartBtn").clicked += () => RestartGame();
        root.Q<Button>("MainMenuBtn").clicked += () => LoadMainMenu();
        root.Q<Button>("SettingsBtn").clicked += () => LoadSettingsScene();
        root.Q<Button>("ScoresBtn").clicked += () => LoadScoreHistory();
    }

    void Update()
    {
        // Toggle pause when 'Escape' or 'P' is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Freeze game time
            root.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.None; // Show cursor
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f; // Resume game time
            root.style.display = DisplayStyle.None;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Hide cursor (for FPS/Action games)
            UnityEngine.Cursor.visible = false;
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Reset time before reloading!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu_Scene"); // Ensure this matches your scene name
    }
    void LoadSettingsScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SettingsScene"); // Ensure this matches your scene name
    }

    void LoadScoreHistory()
    {
        Time.timeScale = 1f;
        root.style.visibility = Visibility.Hidden; // Hide pause menu
        scoreHistoryRoot.style.display = DisplayStyle.Flex;
    }

}
