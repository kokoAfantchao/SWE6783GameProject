using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWalls : MonoBehaviour {

    public GameManager GameManager;
	public GameObject gameModeManager;
	private GameModeManager gameManagerScript;


	void Start()
	{
		gameManagerScript = gameModeManager.GetComponent<GameModeManager>();
	}
	void OnTriggerEnter2D(Collider2D hitInfo) {
		if (hitInfo.name == "Ball Prefab")
		{
			Debug.Log("Ball hit " + transform.name);
			string wallName = transform.name;
			//GameManager.Score(wallName);
			gameManagerScript.GoalScored(wallName);
			hitInfo.gameObject.SendMessage ("RestartGame", 1, SendMessageOptions.RequireReceiver);
		}
	}
	
}
