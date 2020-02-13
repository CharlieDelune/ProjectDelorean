using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

	private PartyController party;
	private InventoryMenuController ic;
	private int toUse;
	private UnitStats target;
	private GameObject sender;

	// Use this for initialization
	void Start () {
		toUse = -1;
		ic = GameObject.Find("MenuControllers").GetComponent<InventoryMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(toUse != -1 && target != null){
			UseItemOnTarget();
		}
	}

	private void UseItemOnTarget(){
		if(Databases.items[toUse].itemType == ItemType.Consumable){
			ConsumableItem consumable = (ConsumableItem)Databases.items[toUse];
			bool used = false;
			if(consumable.targetType == TargetType.One){
				if(consumable.hpRestore > 0 && target.currentHealth < target.maxHealth){
					target.RestoreHp(consumable.hpRestore);
					used = true;
				}
				if(consumable.mpRestore > 0 && target.currentMana < target.maxMana){
					target.RestoreMp(consumable.mpRestore);
					used = true;
				}
			}
			else if(consumable.targetType == TargetType.AllFriendly){
				foreach(GameObject member in party.playerParty){
					UnitStats memberStats = member.GetComponent<UnitStats>();
					if(memberStats.available == true){
						if(consumable.hpRestore > 0 && memberStats.currentHealth < memberStats.maxHealth){
							memberStats.RestoreHp(consumable.hpRestore);
							used = true;
						}
						if(consumable.mpRestore > 0 && memberStats.currentMana < memberStats.maxMana){
							memberStats.RestoreMp(consumable.mpRestore);
							used = true;
						}
					}
				}
			}
			if(used){
				party.RemoveItemFromInventory(toUse);
				sender.GetComponent<InventoryButtonController>().UpdateText();
				ic.UpdateTargetPanel();
				if(party.playerInventoryCount[toUse] < 1){
					sender.GetComponent<InventoryButtonController>().DestroySelf();
					ic.HideTargetPanel();
					toUse = -1;
					sender = null;
				}
				target = null;
			}
		}
		else{
			//SHOW SOME KIND OF WARNING
		}
	}

	public int GetToUse(){
		return toUse;
	}

	public void SetToUse(int i){
		toUse = i;
	}
	public UnitStats GetTarget(){
		return target;
	}
	
	public void SetTarget(UnitStats us){
		target = us;
	}
	public void SetSender(GameObject go){
		sender = go;
	}
}
