using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour {

	private StatusMenuController stat;
	private PartyController party;
	private AbilityController ac;
	private AbilityMenuController able;
	public GameObject holderPanel;
	public int abilityNum;
	public Text nameText;
	public Text descText;
	public Text costText;

	// Use this for initialization
	void Start () {
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		ac = GameObject.Find("ItemControllers").GetComponent<AbilityController>();
		able = GameObject.Find("MenuControllers").GetComponent<AbilityMenuController>();
		gameObject.transform.SetParent(holderPanel.transform,false);
		UpdateText();
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToUse);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Button>().interactable == false){
			((Text)gameObject.GetComponentInChildren<Text>()).color = new Color(0.4f,0.4f,0.4f,1.0f);
		}
		else{
			((Text)gameObject.GetComponentInChildren<Text>()).color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
	}

	private void SendToUse(){
		ac.SetToUse(abilityNum);
		able.ShowTargetPanel();
		able.SetAbilityButtons(false);
	}

	public void UpdateText(){
		nameText.text = party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities[abilityNum].abilityName;
		descText.text = party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities[abilityNum].abilityDesc;
		costText.text = party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities[abilityNum].mpCost.ToString();
	}
}
