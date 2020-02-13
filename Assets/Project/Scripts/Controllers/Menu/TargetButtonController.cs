using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetButtonController : MonoBehaviour {
	private ItemController ic;
	public UnitStats member;
	public GameObject targetPanel;
	public int loc;

	// Use this for initialization
	void Start () {
		ic = GameObject.Find("ItemControllers").GetComponent<ItemController>();
		gameObject.transform.SetParent(targetPanel.transform,false);
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToUse);
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void UpdateText(){
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberName").GetComponent("Text")).text = member.charName;
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberHPValue").GetComponent("Text")).text = member.currentHealth + "/" + member.maxHealth;
		((Text)gameObject.transform.Find("MainPauseMenuPartyMemberMPValue").GetComponent("Text")).text = member.currentMana + "/" + member.maxMana;
	}

	private void SendToUse(){
		ic.SetTarget(member);
	}
}
