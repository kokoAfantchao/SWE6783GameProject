using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour {

	public int PlayerScore1 = 0;
	public int PlayerScore2 = 0;

	public TMP_Text player1ScoreText;
	public TMP_Text player2ScoreText;

	public GUISkin layout;

	GameObject theBall;

	// Use this for initialization
	void Start () {
		theBall = GameObject.FindGameObjectWithTag ("Ball");
	}

	public void Score(string wallID) {
		if (wallID == "RightWall") {
			PlayerScore1++;
			player1ScoreText.text = PlayerScore1.ToString();
		} else {
			PlayerScore2++;
			player2ScoreText.text = PlayerScore2.ToString();
		}
	}

	void OnGUI() {
		GUI.skin = layout;
		GUI.Label (new Rect (Screen.width / 2 - 150 - 12, 20, 100, 100), "" + PlayerScore1);
		GUI.Label (new Rect (Screen.width / 2 + 150 + 12, 20, 100, 100), "" + PlayerScore2);

		if (GUI.Button (new Rect (Screen.width / 2 - 60, 35, 120, 53), "RESTART")) {
			PlayerScore1 = 0;
			PlayerScore2 = 0;
			theBall.SendMessage ("RestartGame", 0.5f, SendMessageOptions.RequireReceiver);
		}

		if (PlayerScore1 == 3) {
			GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER ONE WINS");
			theBall.SendMessage ("ResetBall", null, SendMessageOptions.RequireReceiver);
		} else if (PlayerScore2 == 3) {
			GUI.Label (new Rect (Screen.width / 2 - 150, 200, 2000, 1000), "PLAYER TWO WINS");
			theBall.SendMessage ("ResetBall", null, SendMessageOptions.RequireReceiver);
		}
	}

}
