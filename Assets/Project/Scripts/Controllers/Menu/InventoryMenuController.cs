using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenuController : MonoBehaviour {

	public GameObject inventoryPauseMenu;
	public GameObject menuButtonHolder;
	public GameObject itemPanel;
	public GameObject targetMenu;
	public GameObject inventoryButton;
	public GameObject targetButton;
	public Text coinsValue;

	private PartyController party;
	private MainMenuController main;
	private bool listeningForItem;
	private bool listeningForTarget;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		main = gameObject.GetComponent<MainMenuController>();
	}

	public bool SetPause(){
		if(listeningForTarget){
			HideTargetPanel();
			listeningForTarget = false;
			return true;
		}
		else if(listeningForItem){
			SetInventoryMenuButtons(true);
			SetAllInventoryButtons(false);
			listeningForItem = false;
			return true;
		}
		else if(inventoryPauseMenu.activeSelf == true){
			HideInventoryPanel();
			main.ShowMainPanel();
			return true;
		}
		return false;
	}
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
	public void ShowInventoryMenu () {
		UpdateInventory();
		coinsValue.text = party.coins + "c";
		main.HideMainPanel();
		inventoryPauseMenu.SetActive(true);
		SetInventoryMenuButtons(true);
	}
	public void HideInventoryPanel(){
		listeningForItem = false;
		listeningForTarget = false;
		inventoryPauseMenu.SetActive(false);
	}
	public void UpdateInventory () {
		Button[] buttons = itemPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		if(party.InventoryNotEmpty()){
			for(int i = 0; i < party.playerInventoryCount.Length; i++){
				if(party.playerInventoryCount[i] > 0){
					GameObject btn = Instantiate (inventoryButton);
					InventoryButtonController btnCont = btn.GetComponent<InventoryButtonController>();
					btnCont.invPanel = itemPanel;
					btnCont.invItemNum = i;
				}
			}
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
				TargetButtonController btnCont = btn.GetComponent<TargetButtonController>();
				btnCont.targetPanel = targetMenu;
				btnCont.member = member.GetComponent<UnitStats>();
			}
		}
		targetMenu.SetActive(true);
		SetAllInventoryButtons(false);
		listeningForTarget = true;
	}
	public void HideTargetPanel(){
		targetMenu.SetActive(false);
		SetAllInventoryButtons(true);
		listeningForTarget = false;
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
}
