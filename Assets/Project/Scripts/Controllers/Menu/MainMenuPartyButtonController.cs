using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPartyButtonController : MonoBehaviour {
	private StatusMenuController stat;
	public UnitStats member;
	public GameObject targetPanel;
	public int loc;

	// Use this for initialization
	void Start () {
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		gameObject.transform.SetParent(targetPanel.transform,false);
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateText(){
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberName").GetComponent("Text")).text = member.charName;
		((Image)gameObject.transform.Find("MainPauseMenuPartyMemberPortrait").GetComponent("Image")).sprite = member.battleHead;
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberLevelValue").GetComponent("Text")).text = member.level.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberHPCurrValue").GetComponent("Text")).text = member.currentHealth.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberHPMaxValue").GetComponent("Text")).text = member.maxHealth.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberMPCurrValue").GetComponent("Text")).text = member.currentMana.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberMPMaxValue").GetComponent("Text")).text = member.maxMana.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberExpCurrValue").GetComponent("Text")).text = member.experience.ToString();
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberExpMaxValue").GetComponent("Text")).text = member.experienceToNextLevel.ToString();
	}

	public void SendToManage(){
		stat.SetStatusTarget(loc);
		stat.ShowStatusPanel();
	}
}
