using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Toggle soundToggle;
    public Dropdown difficultyDropdown;

    void Start()
    {
        soundToggle.isOn = PlayerPrefs.GetInt("Sound", 1) == 1;
        difficultyDropdown.value = PlayerPrefs.GetInt("Difficulty", 0);

        ApplySound();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Sound", soundToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Difficulty", difficultyDropdown.value);

        PlayerPrefs.Save();
        ApplySound();
    }

    void ApplySound()
    {
        AudioListener.pause = !soundToggle.isOn;
    }

    public void BackToMenu()
    {
        SaveSettings();
        SceneManager.LoadScene("Main_Menu_Scene");
    }
}