﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode {
	void unlockNode();
	void completeNode();
	Vector3 getPosition();
}

public class Node : MonoBehaviour, INode {

	// Used for the demo - this is the key to press to hack this node
	public string hackKey;

	// A locked node cannot be completed. Once unlocked, a node can be completed which gives access to whatever information that node contains
	public bool isUnlocked = true;
	public bool isCompleted = false;

	// Upon completion, all child nodes will unlock
	public INode[] childNodes;

	// Upon completion, show all objects with this tag.
	public string tagForHiddenObjects;

	// This is a reference to the progress bar of the node's completion
	//TODO dynamically generate this
	public GameObject progressBar;

	// True if the node is actively being hacked
	private bool isHacking;

	int percentComplete = 0;

	// Lines from this node to all of its children
	private LineRenderer[] nodeLines;
	
	private float fadePerSecond = 0.5F;

	void Start () {
		//Hide map icons with given tag
		MapVisibility.setVisibilityForTag (tagForHiddenObjects, false);

		// Draw a line from this node to all children
		nodeLines = new LineRenderer[childNodes.Length];
		
		for(int i=0;i<nodeLines.Length;i++){
			LineRenderer line = this.gameObject.AddComponent<LineRenderer>();
			line.startWidth = 0.2F;
			line.endWidth = 0.2F;
			line.positionCount = 2;
			line.SetPosition(0, gameObject.transform.position);
			line.SetPosition(1, childNodes[i].getPosition());
      		line.material = new Material (Shader.Find("Particles/Additive"));
			line.startColor = Color.red;
			line.endColor = Color.red;

			nodeLines[i] = line;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// If the node is locked, do nothing
		if(isUnlocked) {
			checkForHackKey();

			if(isHacking && percentComplete < 100f) {
				percentComplete++;
				float amt = percentComplete / 100f;

				progressBar.transform.localPosition = new Vector3(amt/2 - 0.5f, progressBar.transform.localPosition.y, progressBar.transform.localPosition.z);
				progressBar.transform.localScale = new Vector3(amt, 1, 1);
			}

			if(!isCompleted && percentComplete >= 100f) {
				completeNode();
			}
		}
	}

	public void unlockNode() {
		isUnlocked = true;
	}

	public Vector3 getPosition() {
		return gameObject.transform.position;
	}

	public void completeNode() {
		// Show all hidden objects
		if(tagForHiddenObjects != null) {
			Debug.Log(tagForHiddenObjects.Length);
			MapVisibility.setVisibilityForTag (tagForHiddenObjects, true);
		}

		// Unlock all child nodes
		for(int i=0;i<childNodes.Length;i++){
			childNodes[i].unlockNode();
		}

		//TODO generate dynamically
		if(progressBar != null){
			// Mark all connections as green, fade them out
			Color currentColor = progressBar.GetComponent<MeshRenderer>().material.color;
			Color newColor = new Color(0F, 255F, 0F, currentColor.a - (fadePerSecond * Time.deltaTime));
			Debug.Log(newColor.a);

			foreach(LineRenderer line in nodeLines){
				line.startColor = newColor;
				line.endColor = newColor;
			}
			progressBar.GetComponent<MeshRenderer>().material.color = newColor;

			// Finally, hide the node from view
			if(newColor.a <= 0) {
				gameObject.SetActive(false);
			}
		}
	}

	//TODO determine if this is actually the best way of doing this. Maybe polling for the key with a timer on increments is better?
	// If we press the key, start hacking
	void checkForHackKey() {
		if (Input.GetKeyDown(hackKey)){
			isHacking = true;
		}

		// When we let go of the key, stop hacking
		if(Input.GetKeyUp(hackKey)) {
			isHacking = false;
		}
	}

}