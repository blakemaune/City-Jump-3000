using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	private MapGenerator mg;
	private KeepScore sb;
	public float elapsed;
	public float timeRemaining;
	public float maxTime;
	public Camera secondary;
	private bool gameOver = false;
	public bool paused = false;
	public Canvas gameOverScreen;
	public Canvas hud;
	public Canvas pauseScreen;

	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
		sb = GameObject.FindGameObjectWithTag ("Score").GetComponent<KeepScore>();
		elapsed = 0f;
		timeRemaining = maxTime;
		paused = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TogglePause ();
		}

		if (timeRemaining > 0f && paused == false) {
			// Come here for time factor
			timeRemaining -= Time.deltaTime;
			elapsed += Time.deltaTime;
		}

		if (timeRemaining <= 0f && gameOver == false) {
			// GAME OVER situation
			timeRemaining = 0.0f;
			secondary.gameObject.SetActive (true);
			Camera.main.gameObject.SetActive(false);
			gameOverScreen.gameObject.SetActive(true);
			hud.GetComponent<Canvas> ().enabled = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			gameOver = true;
		}
		GetComponent<Text> ().text = timeRemaining.ToString ("00.00");
	}

	public void ResetTime(){
		timeRemaining = maxTime;
		GetComponent<Text> ().text = timeRemaining.ToString ("00.00");
	}

	public void AddTime(float t){
		timeRemaining += t;
		GetComponent<Text> ().text = timeRemaining.ToString ("00.00");
	}

	public void TogglePause(){
		paused = !paused;
		if (paused == true) {
			Time.timeScale = 0.0f;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			pauseScreen.gameObject.SetActive (true);
			hud.GetComponent<Canvas>().enabled = false;
		} else {
			Time.timeScale = 1.0f;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			pauseScreen.gameObject.SetActive (false);
			hud.GetComponent<Canvas>().enabled = true;
		}
	}
}
