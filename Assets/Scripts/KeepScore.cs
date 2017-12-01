using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour {
	public int score;

	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text> ().text = "Score: " + score.ToString ();
	}

	public string finalStats(){
		Timer t = GameObject.Find ("Timer").GetComponent<Timer> ();
		string output = "You scored " + score + " in " + t.elapsed.ToString ("00.00") + " seconds!";
		return output;
	}
}
