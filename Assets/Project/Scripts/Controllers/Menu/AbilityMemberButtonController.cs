using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMemberButtonController : MonoBehaviour {
	private AbilityController ability;
	public UnitStats member;
	public GameObject parentPanel;
	public int loc;

	// Use this for initialization
	void Start () {
		ability = GameObject.Find("ItemControllers").GetComponent<AbilityController>();
		gameObject.transform.SetParent(parentPanel.transform,false);
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToAbility);
		UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateText(){
		((Text)gameObject.transform.Find("OrderMenuPartyMemberName").GetComponent("Text")).text = member.charName;
		((Image)gameObject.transform.Find("OrderMenuPartyMemberPortrait").GetComponent("Image")).sprite = member.battleHead;
		((Text)gameObject.transform.Find("OrderMenuPartyMemberHPCurrValue").GetComponent("Text")).text = member.currentHealth.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberHPMaxValue").GetComponent("Text")).text = member.maxHealth.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberMPCurrValue").GetComponent("Text")).text = member.currentMana.ToString();
		((Text)gameObject.transform.Find("OrderMenuPartyMemberMPMaxValue").GetComponent("Text")).text = member.maxMana.ToString();
	}

	public void SendToAbility(){
		ability.SetTarget(member);
	}
}
