using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour {
	public int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		FindObjectOfType<Text> ().text = "Score: " + score;
	}

	public void IncrementScore(){
		score++;
	}
}
