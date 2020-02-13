using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundaryTrigger : MonoBehaviour {

	public string toLoad;
	public Vector3 nextPosition;

	private UIController uI;

	// Use this for initialization
	void Start () {
		uI = GameObject.Find("UIController").GetComponent<UIController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Player"){
			other.gameObject.GetComponent<PlayerBehavior>().allowingInput=false;
			other.gameObject.GetComponent<PlayerBehavior>().StopMovement();
			other.gameObject.GetComponent<PlayerBehavior>().inCutscene=true;
			StartCoroutine(FadeThenChangeScenes());
		}
	}
	public IEnumerator FadeThenChangeScenes(){
		yield return null;
		SceneMgmt.SetLocation(nextPosition);
		yield return StartCoroutine(uI.FadeToBlackCoroutine());
		//yield return new WaitForSeconds(1.0f);
		SceneMgmt.LoadScene(toLoad);
	}
}
