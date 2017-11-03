﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMenu : NetworkBehaviour {

	public GameObject waitingCamera;

	public NetworkManagerHUD netManagerHUD;
	public PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (playerManager.getNumberOfConnections() >= 1)
		{
			waitingCamera.SetActive (false);

			netManagerHUD.enabled = false;		//Disable the network manager HUD once we are connected
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			//open in-game menu (currently non-existent)
		}
	}
}