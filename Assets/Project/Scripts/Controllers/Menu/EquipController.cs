using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController : MonoBehaviour {

	private PartyController party;
	private StatusMenuController status;
	private int toUse;
	private UnitStats target;
	private string equipLoc;

	// Use this for initialization
	void Start () {
		toUse = -1;
		equipLoc = "";
		status = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(toUse != -1 && equipLoc != "" && target != null){
			UseItemOnTarget();
		}
	}

	private void UseItemOnTarget(){
		if(Databases.items[toUse].itemType == ItemType.Equipable){
			string equipable = Databases.items[toUse].itemName;
			bool used = false;
			if(equipLoc == "weapon"){
				if(target.weapon != ""){
					party.AddItemToInventory(Databases.FindItem(target.weapon),1);
				}
				target.weapon = equipable;
				used = true;
			}
			else if(equipLoc == "armor"){
				if(target.armor != ""){
					party.AddItemToInventory(Databases.FindItem(target.armor),1);
				}
				target.armor = equipable;
				used = true;
			}
			else if(equipLoc == "acc1"){
				if(target.accessory1 != ""){
					party.AddItemToInventory(Databases.FindItem(target.accessory1),1);
				}
				target.accessory1 = equipable;
				used = true;
			}
			else if(equipLoc == "acc2"){
				if(target.accessory2 != ""){
					party.AddItemToInventory(Databases.FindItem(target.accessory2),1);
				}
				target.accessory2 = equipable;
				used = true;
			}
			if(used){
				party.RemoveItemFromInventory(toUse);
				toUse = -1;
				equipLoc = "";
			}
			target.CalculateModStats();
			status.DisableEquipButtons();
			status.UpdateStatusText();
		}
		else{
			//SHOW SOME KIND OF WARNING
		}
	}
	public void Unequip(){
		if(equipLoc == "weapon"){
			if(target.weapon != ""){
				party.AddItemToInventory(Databases.FindItem(target.weapon),1);
			}
			target.weapon = "";
		}
		else if(equipLoc == "armor"){
			if(target.armor != ""){
				party.AddItemToInventory(Databases.FindItem(target.armor),1);
			}
			target.armor = "";
		}
		else if(equipLoc == "acc1"){
			if(target.accessory1 != ""){
				party.AddItemToInventory(Databases.FindItem(target.accessory1),1);
			}
			target.accessory1 = "";
		}
		else if(equipLoc == "acc2"){
			if(target.accessory2 != ""){
				party.AddItemToInventory(Databases.FindItem(target.accessory2),1);
			}
			target.accessory2 = "";
		}
		target.CalculateModStats();
		status.DisableEquipButtons();
		status.UpdateStatusText();
	}
	public int GetToUse(){
		return toUse;
	}
	public void SetToUse(int i){
		toUse = i;
	}

	public string GetEquipLoc(){
		return equipLoc;
	}

	public void SetEquipLoc(string s){
		equipLoc = s;
	}

	public UnitStats GetTarget(){
		return target;
	}
	
	public void SetTarget(UnitStats us){
		target = us;
	}
	public bool CanEquip(string[] s){
		bool equipable = false;
		foreach(string str in s){
			if(str == target.charName){
				equipable = true;
				break;
			}
		}
		return equipable;
	}
}
