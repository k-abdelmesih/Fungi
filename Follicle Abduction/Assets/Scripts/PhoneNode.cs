﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneNode : Node {

	public AudioClip ring;
	public Patrol[] guards;

	private AudioSource source;
	private GameObject phone;
	private bool ringing;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		ringing = false;
	}
	
	// Update is called once per frame
	void Update () {
		//temp while waiting for ray-picking to work
		if (ringing && !source.isPlaying) {
			StopRinging ();
		}
		if (Input.GetKeyUp (KeyCode.P)) { //for debugging in human mode!
			StartAction ();
		}
	}

	public override void StartAction() {
		if (state == NodeState.UNLOCKED) {
			Ring ();
		}
	}

	public override void EndAction() {
		return;
	}

	void Ring() {
		if (!ringing) {
			source.PlayOneShot (ring);
			ringing = true;
			foreach (Patrol guard in guards) {
				GameObject parent = transform.parent.gameObject;
				print (guard);
				if (parent) {
					guard.AddSecondaryTarget (parent);
				}
			}
		}
	}

	void StopRinging () {
		if (ringing) {
			ringing = false;
			foreach (Patrol guard in guards) {
				GameObject parent = transform.parent.gameObject;
				print (parent);
				if (parent) {
					guard.RemoveSecondaryTarget (parent);
				}
			}
		}
	}
}