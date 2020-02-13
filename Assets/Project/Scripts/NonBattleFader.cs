using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonBattleFader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0,-9.8f,0);
		//GameObject.Find("UIController").GetComponent<UIController>().needToFadeIn = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
