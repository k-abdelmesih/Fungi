﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

	public int maxSuspiciousness = 100;
	private int suspiciousness;
	private bool captured;

	// Use this for initialization
	void Start () {
		suspiciousness = 0;
		captured = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (suspiciousness >= maxSuspiciousness) {
			captured = true;
		}
	}

	public void doSomethingSuspicious(int howSuspicious) {
		suspiciousness += howSuspicious;
	}

	public int getSuspiciousness(){
		return suspiciousness;
	}

	public bool hasBeenCaptured(){
		return captured;
	}
}