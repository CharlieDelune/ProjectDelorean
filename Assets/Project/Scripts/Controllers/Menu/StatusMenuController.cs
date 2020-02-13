using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenuController : MonoBehaviour {

	public GameObject statusPauseMenu;
	public GameObject changeEquipPanel;
	public GameObject unequipButton;
	public GameObject equipItemButton;
	public GameObject changeEquipItems;
	public GameObject nextButton;
	public GameObject prevButton;
	public int statusTarget;
	private MainMenuController main;
	private PartyController party;
	private EquipController eq;
	private bool listeningForEquip;
	private bool listeningForSlot;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		eq = GameObject.Find("ItemControllers").GetComponent<EquipController>();
		main = GameObject.Find("MenuControllers").GetComponent<MainMenuController>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool SetPause(){
		if(listeningForEquip){
			HideChangeEquipMenu();
			listeningForEquip = false;
			return true;
		}
		else if(listeningForSlot){
			DisableEquipButtons();
			listeningForSlot = false;
			return true;
		}
		else if(statusPauseMenu.activeSelf == true){
			DisableEquipButtons();
			HideStatusPanel();
			main.ShowMainPanel();
			return true;
		}
		return false;
	}
	public void ShowStatusPanel(){
		main.HideMainPanel();
		statusPauseMenu.SetActive(true);
		UpdateStatusText();
	}
	public void HideStatusPanel(){
		statusPauseMenu.SetActive(false);
		statusTarget = -1;
	}
	public void UpdateStatusText(){
		if(statusTarget >= 0 && statusTarget < party.playerParty.Length){
			UnitStats targetStats = party.playerParty[statusTarget].GetComponent<UnitStats>();
			if(targetStats != null){
				GameObject.Find("StatusPauseMenuMemberName").GetComponent<Text>().text = targetStats.charName;
				GameObject.Find("StatusPauseMenuPortraitImage").GetComponent<Image>().sprite = targetStats.battleHead;
				GameObject.Find("StatusPauseMenuMemberLevelValue").GetComponent<Text>().text = targetStats.level.ToString();
				GameObject.Find("StatusPauseMenuMemberHPCurrValue").GetComponent<Text>().text = targetStats.currentHealth.ToString();
				GameObject.Find("StatusPauseMenuMemberHPMaxValue").GetComponent<Text>().text = targetStats.maxHealth.ToString();
				GameObject.Find("StatusPauseMenuMemberMPCurrValue").GetComponent<Text>().text = targetStats.currentMana.ToString();
				GameObject.Find("StatusPauseMenuMemberMPMaxValue").GetComponent<Text>().text = targetStats.maxMana.ToString();
				GameObject.Find("StatusPauseMenuMemberExpCurrValue").GetComponent<Text>().text = targetStats.experience.ToString();
				GameObject.Find("StatusPauseMenuMemberExpMaxValue").GetComponent<Text>().text = targetStats.experienceToNextLevel.ToString();
				if(targetStats.weapon != ""){
					GameObject.Find("StatusPauseMenuMemberWeaponValue").GetComponent<Text>().text = ((EquipableItem)Databases.items[Databases.FindItem(targetStats.weapon)]).itemName;
				}
				else{
					GameObject.Find("StatusPauseMenuMemberWeaponValue").GetComponent<Text>().text = "(None)";
				}
				if(targetStats.armor != ""){
					GameObject.Find("StatusPauseMenuMemberArmorValue").GetComponent<Text>().text = ((EquipableItem)Databases.items[Databases.FindItem(targetStats.armor)]).itemName;
				}
				else{
					GameObject.Find("StatusPauseMenuMemberArmorValue").GetComponent<Text>().text = "(None)";
				}
				if(targetStats.accessory1 != ""){
					GameObject.Find("StatusPauseMenuMemberAccessory1Value").GetComponent<Text>().text = ((EquipableItem)Databases.items[Databases.FindItem(targetStats.accessory1)]).itemName;
				}
				else{
					GameObject.Find("StatusPauseMenuMemberAccessory1Value").GetComponent<Text>().text = "(None)";
				}
				if(targetStats.accessory2 != ""){
					GameObject.Find("StatusPauseMenuMemberAccessory2Value").GetComponent<Text>().text = ((EquipableItem)Databases.items[Databases.FindItem(targetStats.accessory2)]).itemName;
				}
				else{
					GameObject.Find("StatusPauseMenuMemberAccessory2Value").GetComponent<Text>().text = "(None)";
				}
				GameObject.Find("StatusPauseMenuMemberModAtkValue").GetComponent<Text>().text = targetStats.modAttack.ToString();
				GameObject.Find("StatusPauseMenuMemberAtkValue").GetComponent<Text>().text = "(" + targetStats.baseAttack.ToString() + ")";
				GameObject.Find("StatusPauseMenuMemberModDefValue").GetComponent<Text>().text = targetStats.modDefense.ToString();
				GameObject.Find("StatusPauseMenuMemberDefValue").GetComponent<Text>().text =  "(" + targetStats.baseDefense.ToString() + ")";
				GameObject.Find("StatusPauseMenuMemberModMagAtkValue").GetComponent<Text>().text = targetStats.modMagAttack.ToString();
				GameObject.Find("StatusPauseMenuMemberMagAtkValue").GetComponent<Text>().text =  "(" + targetStats.baseMagAttack.ToString() + ")";
				GameObject.Find("StatusPauseMenuMemberModMagDefValue").GetComponent<Text>().text = targetStats.modMagDefense.ToString();
				GameObject.Find("StatusPauseMenuMemberMagDefValue").GetComponent<Text>().text =  "(" + targetStats.baseMagDefense.ToString() + ")";
				GameObject.Find("StatusPauseMenuMemberModAgiValue").GetComponent<Text>().text = targetStats.modAgility.ToString();
				GameObject.Find("StatusPauseMenuMemberAgiValue").GetComponent<Text>().text =  "(" + targetStats.baseAgility.ToString() + ")";
				GameObject.Find("StatusPauseMenuPreviousButton").GetComponent<NextPrevButtonController>().currentIndex = statusTarget;
				GameObject.Find("StatusPauseMenuPreviousButton").GetComponent<NextPrevButtonController>().GetLocation();
				GameObject.Find("StatusPauseMenuNextButton").GetComponent<NextPrevButtonController>().currentIndex = statusTarget;
				GameObject.Find("StatusPauseMenuNextButton").GetComponent<NextPrevButtonController>().GetLocation();
				eq.SetTarget(party.playerParty[statusTarget].GetComponent<UnitStats>());
			}
		}
	}
	public void ChangeEquipButtonPressed(){
		EnableEquipButtons();
	}
	public void ShowChangeEquipMenu () {
		changeEquipPanel.SetActive(true);
		listeningForEquip = true;
		UpdateChangeEquip();
	}
	public void HideChangeEquipMenu(){
		changeEquipPanel.SetActive(false);
		listeningForEquip = false;
	}
	public void DisableMainButtons(){
		Button[] buttons = GameObject.Find("StatusPauseMenuButtonPanel").GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = false;
		}
		prevButton.GetComponent<Button>().interactable = false;
		nextButton.GetComponent<Button>().interactable = false;
	}
	public void EnableMainButtons(){
		Button[] buttons = GameObject.Find("StatusPauseMenuButtonPanel").GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = true;
		}
		prevButton.GetComponent<Button>().interactable = true;
		nextButton.GetComponent<Button>().interactable = true;
	}
	public void EnableEquipButtons () {
		DisableMainButtons();
		Button[] buttons = GameObject.Find("StatsPauseMenuEquipValuePanel").GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = true;
		}
		listeningForSlot = true;
	}
	public void DisableEquipButtons () {
		EnableMainButtons();
		Button[] buttons = GameObject.Find("StatsPauseMenuEquipValuePanel").GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = false;
		}
		listeningForSlot = false;
	}
	public void UpdateChangeEquip () {
		Button[] buttons = changeEquipPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		GameObject unbtn = Instantiate (unequipButton);
		UnequipItemButtonController unbtnCont = unbtn.GetComponent<UnequipItemButtonController>();
		unbtnCont.invPanel = changeEquipItems;
		unbtn.GetComponent<Button>().interactable = true;
		if(party.InventoryNotEmpty()){
			for(int i = 0; i < party.playerInventoryCount.Length; i++){
				if(party.playerInventoryCount[i] > 0 && Databases.items[i].itemType == ItemType.Equipable && ((EquipableItem)Databases.items[i]).equipLoc == eq.GetEquipLoc() && eq.CanEquip(((EquipableItem)Databases.items[i]).equippableBy)){
					GameObject btn = Instantiate (equipItemButton);
					EquipItemButtonController btnCont = btn.GetComponent<EquipItemButtonController>();
					btnCont.invPanel = changeEquipItems;
					btnCont.invItemNum = i;
					btn.GetComponent<Button>().interactable = true;
				}

			}
		}
	}
	public void SetStatusTarget(int i){statusTarget = i;}
}
