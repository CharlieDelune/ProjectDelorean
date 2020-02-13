using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardTrees : MonoBehaviour {
	private Transform mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = -mainCamera.forward;
	}
}
