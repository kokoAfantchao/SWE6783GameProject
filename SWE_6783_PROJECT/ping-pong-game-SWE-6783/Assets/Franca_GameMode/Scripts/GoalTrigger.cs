using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public bool isLeftGoal;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            GameModeManager.Instance.GoalScored(isLeftGoal);
        }
    }
}
