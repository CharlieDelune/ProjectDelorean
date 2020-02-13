using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDamageHealTextController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void UpdateText(string s){
		gameObject.GetComponentInChildren<Text>().text = s;
	}
	public void StartAnimation(){
		StartCoroutine(MoveAndWaitAndDestroy());
	}
	IEnumerator MoveAndWaitAndDestroy(){
		float t = 0;
		while(t<2){
			transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y + (.03f * Time.deltaTime), transform.localPosition.z);
			t += Time.deltaTime;
			yield return 0;
		}
		Destroy(gameObject);
	}
}
