using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioSource clickerSound;
    void Start()
    {
        clickerSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clickerSound.Play();
        }
    }
    public void PlayGame(){
        SceneManager.LoadScene("Level_One");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }


    public void QuitGame(){
        Application.Quit();
    }
}
