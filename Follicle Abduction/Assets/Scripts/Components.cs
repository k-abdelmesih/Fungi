﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour {

	[SerializeField]
	Behaviour[] components;


	public Behaviour[] GetComponentList()
	{
		Debug.Log("Test");
		return components;
	}
}
