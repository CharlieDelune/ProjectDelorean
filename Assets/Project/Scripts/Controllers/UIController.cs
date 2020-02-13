using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	private PlayerBehavior pb;
	public GameObject textHolder;
	public Image dialogueHeadHolder;
	public Text dialogueText;
	public Text dialogueTitle;
	public GameObject choiceHolder;
	public Text yesText;
	public Text noText;
	public Image fadeToBlack;

	// Use this for initialization
	void Start () {
		pb = GameObject.Find("Player").GetComponent<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if(textHolder.activeSelf || choiceHolder.activeSelf){
			pb.SetAllowingInput(false);
		}
	}
	public void ShowTextHolder(){
		textHolder.SetActive(true);
		pb.SetAllowingInput(false);
	}

	public void SetDialogueText(string toSet){
		dialogueText.text = toSet;
	}

	public void SetDialogueHeadSprite(Sprite img){
		if(img != null){
			dialogueHeadHolder.enabled = true;
			dialogueHeadHolder.sprite = img;
		}
		else{
			dialogueHeadHolder.sprite = null;
			dialogueHeadHolder.enabled = false;
		}
	}

	public void SetDialogueTitle(string toSet){
		if(toSet != ""){
			dialogueTitle.enabled = true;
			dialogueTitle.text = toSet;
		}
		else{
			dialogueTitle.enabled = false;
			dialogueTitle.text = "";
		}
	}

	public void HideTextHolder(){
		textHolder.SetActive(false);
		pb.SetAllowingInput(true);
	}

	public void ShowChoiceHolder(){
		choiceHolder.SetActive(true);
		pb.SetAllowingInput(false);
	}	

	public void HideChoiceHolder(){
		choiceHolder.SetActive(false);
		pb.SetAllowingInput(true);
	}

	public void SetChoiceText(string toYes,string toNo){
		yesText.text = toYes;
		noText.text = toNo;
	}
	public void FadeOut(){
		StartCoroutine(FadeToBlackCoroutine());
	}
	public IEnumerator FadeToBlackCoroutine(){
		Color temp2 = fadeToBlack.color;
		temp2.a = 0.0f;
		fadeToBlack.color = temp2;
		ShowBlackScreen(true);
		float time = 0;
		while(time < 1.0f){
			time += Time.deltaTime;
			Color temp = fadeToBlack.color;
			temp.a += Time.deltaTime;
			fadeToBlack.color = temp;
			yield return null;
		}
	}
	public void FadeIn(){
		StartCoroutine(FadeFromBlackCoroutine());
	}
	public IEnumerator FadeFromBlackCoroutine(){
		Color temp2 = fadeToBlack.color;
		temp2.a = 1.0f;
		fadeToBlack.color = temp2;
		ShowBlackScreen(true);
		float time = 0;
		while(time < 1.0f){
			time += Time.deltaTime;
			Color temp = fadeToBlack.color;
			temp.a -= Time.deltaTime;
			fadeToBlack.color = temp;
			yield return null;
		}
		yield return null;
		ShowBlackScreen(false);
	}
	public void ShowBlackScreen(bool act){
		fadeToBlack.gameObject.SetActive(act);
	}
}