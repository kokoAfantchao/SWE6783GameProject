using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Scriptable Objects/DifficultyData")]
public class DifficultyData : ScriptableObject
{
    [Header("Player Settings")]
    public float playerSpeed;
    public float playerYScale;

    [Header("AI Settings")]
    public float aiSpeed;
    public float aiYScale;

    [Header("Ball Settings")]
    public float ballSpeed;

}
