using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Level_One");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
