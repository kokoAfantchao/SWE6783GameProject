using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuController : MonoBehaviour
{
    private VisualElement root;
    private bool isPaused = false;
    private ScoreHistoryController scoreHistoryController;
    private VisualElement scoreHistoryRoot;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

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
        root.Q<Button>("ResumeBtn").clicked += () => { PlayClick(); TogglePause(); };
        root.Q<Button>("RestartBtn").clicked += () => { PlayClick(); RestartGame(); };
        root.Q<Button>("MainMenuBtn").clicked += () => { PlayClick(); LoadMainMenu(); };
        root.Q<Button>("SettingsBtn").clicked += () => { PlayClick(); LoadSettingsScene(); };
        root.Q<Button>("ScoresBtn").clicked += () => { PlayClick(); LoadScoreHistory(); };
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

    private void PlayClick()
    {
        if (audioSource != null && clickSound != null)
            audioSource.PlayOneShot(clickSound);
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
