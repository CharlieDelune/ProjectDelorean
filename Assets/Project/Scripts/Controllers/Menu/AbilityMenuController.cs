using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenuController : MonoBehaviour {

	public GameObject holderPanel;
	public GameObject abilityPanel;
	public GameObject abilityButton;
	public GameObject targetMenu;
	public GameObject targetButton;
	private PartyController party;
	private StatusMenuController stat;
	private AbilityController able;
	private bool listeningForTarget;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		able = GameObject.Find("ItemControllers").GetComponent<AbilityController>();
	}

	public bool SetPause(){
		if(listeningForTarget){
			HideTargetPanel();
			SetAbilityButtons(true);
			return true;
		}
		else if(holderPanel.activeSelf){
			HideAbilityPanel();
			return true;
		}
		return false;
	}
	public void ShowAbilityPanel () {
		stat.DisableMainButtons();
		UpdateAbilities();
		able.SetUser(party.playerParty[stat.statusTarget].GetComponent<UnitStats>());
		holderPanel.SetActive(true);
	}
	public void HideAbilityPanel(){
		Button[] buttons = abilityPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		stat.EnableMainButtons();
		able.SetUser(null);
		holderPanel.SetActive(false);
	}
	public void UpdateAbilities () {
		Button[] buttons = abilityPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		for(int i = 0; i < party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities.Count; i++){
			GameObject btn = Instantiate (abilityButton);
			AbilityButtonController btnCont = btn.GetComponent<AbilityButtonController>();
			btnCont.holderPanel = abilityPanel;
			btnCont.abilityNum = i;
			if(party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities[i].useableOutOfCombat && (party.playerParty[stat.statusTarget].GetComponent<UnitStats>().currentMana >= party.playerParty[stat.statusTarget].GetComponent<UnitStats>().charAbilities[i].mpCost)){
				btn.GetComponent<Button>().interactable = true;
			}
		}
	}
	public void SetAbilityButtons(bool b){
		Button[] buttons = abilityPanel.GetComponentsInChildren<Button>();
		foreach(Button bt in buttons){
			bt.interactable = b;
		}
	}
	public void ShowTargetPanel(){
		Button[] buttons = targetMenu.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		for(int i = 0; i < party.playerParty.Length;i++){
			GameObject member = party.playerParty[i];
			if(member.GetComponent<UnitStats>().available){
				GameObject btn = Instantiate (targetButton);
				AbilityMemberButtonController btnCont = btn.GetComponent<AbilityMemberButtonController>();
				btnCont.parentPanel = targetMenu;
				btnCont.member = member.GetComponent<UnitStats>();
			}
		}
		targetMenu.SetActive(true);
		SetAbilityButtons(false);
		listeningForTarget = true;
	}
	public void HideTargetPanel(){
		targetMenu.SetActive(false);
		SetAbilityButtons(true);
		listeningForTarget = false;
	}
	public void UpdateTargetPanel(){
		Button[] buttons = targetMenu.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.gameObject.GetComponent<AbilityMemberButtonController>().UpdateText();
		}
	}
	/*
	public void SetAllInventoryButtons(bool toSet){
		Button[] buttons = itemPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			if(Databases.items[b.GetComponent<InventoryButtonController>().invItemNum].itemType == ItemType.Consumable && ((ConsumableItem)Databases.items[b.GetComponent<InventoryButtonController>().invItemNum]).useableOutOfCombat == true){
				b.interactable = toSet;
			}
		}
		listeningForItem = toSet;
	}
	public void SetInventoryMenuButtons(bool toSet){
		Button[] btns = menuButtonHolder.GetComponentsInChildren<Button>();
		foreach(Button b in btns){
			b.interactable = toSet;
		}
	}
	public void UpdateTargetPanel(){
		Button[] buttons = targetMenu.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.GetComponent<TargetButtonController>().UpdateText();
		}
	}
	public void UseItemClicked(){
		if(party.InventoryNotEmpty()){
			SetAllInventoryButtons(true);
			SetInventoryMenuButtons(false);
		}
	}
	*/
}
