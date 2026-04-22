using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    private VisualElement root;
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        var backButton = root.Q<Button>("backButton");
        backButton.clicked += () =>
        {
            SceneManager.LoadScene("Main_Menu_Scene"); // replace with your scene name
        };
    }
}