using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusChooserMemberButtonController : MonoBehaviour {
	private StatusMenuController stat;
	private StatusChooserMenuController chooser;
	public UnitStats member;
	public GameObject parentPanel;
	public int loc;

	// Use this for initialization
	void Start () {
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		chooser = GameObject.Find("MenuControllers").GetComponent<StatusChooserMenuController>();
		gameObject.transform.SetParent(parentPanel.transform,false);
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToManage);
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateText(){
		((Text)gameObject.transform.Find("OrderMenuPartyMemberName").GetComponent("Text")).text = member.charName;
		((Image)gameObject.transform.Find("OrderMenuPartyMemberPortrait").GetComponent("Image")).sprite = member.battleHead;
		((Text)gameObject.transform.Find("OrderMenuPartyMemberLevelValue").GetComponent("Text")).text = member.level.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberHPCurrValue").GetComponent("Text")).text = member.currentHealth.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberHPMaxValue").GetComponent("Text")).text = member.maxHealth.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberMPCurrValue").GetComponent("Text")).text = member.currentMana.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberMPMaxValue").GetComponent("Text")).text = member.maxMana.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberExpCurrValue").GetComponent("Text")).text = member.experience.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberExpMaxValue").GetComponent("Text")).text = member.experienceToNextLevel.ToString();
	}

	public void SendToManage(){
		stat.SetStatusTarget(loc);
		stat.ShowStatusPanel();
		chooser.HideManageChooserPanel();
	}
}
