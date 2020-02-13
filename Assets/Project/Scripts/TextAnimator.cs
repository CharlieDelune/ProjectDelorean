using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour {

	public Text uiText;
	public float messageSpeed;

	private string message;

	// Use this for initialization
	void Start () {
		message = uiText.text;
		StartCoroutine(AnimateText());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator AnimateText(){
		while(true){
			uiText.text = "";
			foreach(char c in message.ToCharArray()){
				yield return new WaitForSeconds(messageSpeed);
				uiText.text += c;
				yield return null;
			}
			yield return null;
		}
	}
}
