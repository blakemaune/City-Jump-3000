using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDisp : MonoBehaviour {
	public Text score;
	private KeepScore s;

	void Update () {
		s = score.GetComponent<KeepScore> ();
		this.GetComponent<Text> ().text = s.finalStats();
	}
}
