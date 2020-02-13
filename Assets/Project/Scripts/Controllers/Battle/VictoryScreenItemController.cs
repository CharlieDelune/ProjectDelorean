using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenItemController : MonoBehaviour {
	public Text itemName;
	public Text itemCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetParameters(GameObject go, string name, int count){
		gameObject.transform.SetParent(go.transform);
		gameObject.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,0.0f);
		itemName.text = name;
		itemCount.text = "x" + count;
	}
}
