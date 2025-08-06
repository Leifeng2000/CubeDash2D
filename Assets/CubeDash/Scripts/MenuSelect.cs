using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {

	public GameObject mainUI;
	public GameObject gameplayUI;
	public GameObject pauseUI;
	public GameObject pauseButton;
	public GameObject gameOverUI;
	private AudioSource buttonClick;

	private PlayerMovement playerMovement;
	private SpawnObstacles spawnObstacles;

	void Start() {
		playerMovement = GameObject.Find ("square").GetComponent<PlayerMovement> ();
		spawnObstacles = GameObject.Find ("Canvas").GetComponent<SpawnObstacles> ();

		buttonClick = GameObject.Find ("buttonClick").GetComponent<AudioSource> ();
		mainUI.transform.Find ("bestScore").GetComponent<Text> ().text = "best score: " + PlayerPrefs.GetInt ("bestScore", 0);
	}

	public void play() {
		playerMovement.enabled = true;
		spawnObstacles.enabled = true;
		mainUI.SetActive (false);
		gameplayUI.SetActive (true);
		pauseButton.SetActive (true);
		buttonClick.Play ();
	}
	public void pause() {
		playerMovement.enabled = false;
		Time.timeScale = 0;
		pauseUI.SetActive (true);
		pauseButton.SetActive (false);
		buttonClick.Play ();
	}
	public void resume() {
		Time.timeScale = 1;
		pauseUI.SetActive (false);
		pauseButton.SetActive (true);
		playerMovement.enabled = true;
		buttonClick.Play ();
	}
	public void gameOver() {
		GameObject.Find ("gameOver").GetComponent<AudioSource> ().Play ();
		if (playerMovement.score > PlayerPrefs.GetInt ("bestScore", 0)) {
			PlayerPrefs.SetInt ("bestScore", playerMovement.score);
		}
		gameOverUI.SetActive (true);
		pauseButton.SetActive (false);
		Time.timeScale = 0;
	}
	public void restart() {
		Time.timeScale = 1;
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("obstacle");
		for (int i = 0; i < obstacles.Length; i++) {
			Destroy (obstacles [i]);
		}
		GameObject.Find ("square").transform.position = new Vector2 (-3, 0.6f);
		playerMovement.score = 0;
		playerMovement.up = true;
		playerMovement.positionX = -6;
		playerMovement.positionY = 0.6f;
		playerMovement.speedUp = 0.01f;
		playerMovement.timer = 0;

		GameObject.Find("score").GetComponent<Text> ().text = "score: 0";
		GameObject.Find ("Canvas").GetComponent<SpawnObstacles> ().deletedObstacleLevel = 0;
		GameObject.Find ("Canvas").GetComponent<SpawnObstacles> ().obstacleLevel = 1;
		GameObject.Find ("Canvas").GetComponent<SpawnObstacles> ().lastObstaclePosition = 20.48f;

		playerMovement.enabled = false;
		spawnObstacles.enabled = false;
		mainUI.SetActive (true);
		gameplayUI.SetActive (false);
		mainUI.transform.Find ("bestScore").GetComponent<Text> ().text = "best score: " + PlayerPrefs.GetInt ("bestScore", 0);
		gameOverUI.SetActive (false);
		buttonClick.Play ();
	}
}
