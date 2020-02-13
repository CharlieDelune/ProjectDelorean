using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartyController : MonoBehaviour {

	public GameObject[] playerParty;
	public int[] playerInventoryCount = new int[Databases.items.Length];
	public Dictionary<string, bool> flags;
	public int coins;
	public PlayerBehavior player;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		flags = new Dictionary<string,bool>(){{"SentinelTrue",true},{"SentinelFalse",false},{"KodaInParty",true},{"BhirtApproached",false},{"searchingForMayor",false},{"foundMayor",false}};
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddItemToInventory(string itemToAdd, int numberToAdd){
		int loc = Databases.FindItem(itemToAdd);
		playerInventoryCount[loc] += numberToAdd;
	}
	public void AddItemToInventory(int itemLocation, int numberToAdd){
		playerInventoryCount[itemLocation] += numberToAdd;
	}
	public void RemoveItemFromInventory(string itemToRemove){
		int loc = Databases.FindItem(itemToRemove);
		playerInventoryCount[loc] -= 1;
		if(playerInventoryCount[loc] < 1){
			playerInventoryCount[loc] = 0;
		}
	}
	public void RemoveItemFromInventory(int itemToRemove){
		playerInventoryCount[itemToRemove] -= 1;
		if(playerInventoryCount[itemToRemove] < 1){
			playerInventoryCount[itemToRemove] = 0;
		}
	}
	public void RemoveItemFromInventory(string itemToRemove, int numberToRemove){
		int loc = Databases.FindItem(itemToRemove);
		playerInventoryCount[loc] -= numberToRemove;
		if(playerInventoryCount[loc] < 1){
			playerInventoryCount[loc] = 0;
		}
	}
	public void RemoveItemFromInventory(int itemToRemove, int numberToRemove){
		playerInventoryCount[itemToRemove] -= numberToRemove;
		if(playerInventoryCount[itemToRemove] < 1){
			playerInventoryCount[itemToRemove] = 0;
		}
	}
	public void DeleteItemFromInventory(string itemToRemove){
		int loc = Databases.FindItem(itemToRemove);
		playerInventoryCount[loc] = 0;
	}
	public bool HaveItem(string itemToFind){
		int loc = Databases.FindItem(itemToFind);
		if(playerInventoryCount[loc] > 0){
			return true;
		}
		else{
			return false;
		}
	}
	public bool InventoryNotEmpty(){
		foreach(int i in playerInventoryCount){
			if(i > 0){return true;}
		}
		return false;
	}
	public void SwapPartyMembers(int a, int b){
		GameObject holder = playerParty[a];
		playerParty[a] = playerParty[b];
		playerParty[b] = holder;
	}
	public UnitStats GetUnitStats(string name){
		foreach(GameObject g in playerParty){
			UnitStats u = g.GetComponent<UnitStats>();
			if(u.charName == name){
				return u;
			}
		}
		return null;
	}
}
