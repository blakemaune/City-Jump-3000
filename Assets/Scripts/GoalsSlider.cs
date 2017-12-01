﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsSlider : MonoBehaviour {
	private MapGenerator mg;
	public Text indicator;

	// Use this for initialization
	void Start () {
		mg = GameObject.FindGameObjectWithTag ("MapSpawner").GetComponent<MapGenerator>();
	}

	// Update is called once per frame
	void Update () {
		float fvalue = this.GetComponent<Slider> ().value;
		int ivalue = Mathf.RoundToInt (fvalue);
		mg.goalCount = ivalue;
		indicator.text = ivalue.ToString ();
	}
}
