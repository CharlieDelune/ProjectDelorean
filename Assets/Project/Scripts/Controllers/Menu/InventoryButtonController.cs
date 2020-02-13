using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonController : MonoBehaviour {

	private InventoryMenuController inv;
	private PartyController party;
	private ItemController ic;
	public GameObject invPanel;
	public int invItemNum;
	public Text invText;
	public Text invDesc;
	public Text invCount;

	// Use this for initialization
	void Start () {
		inv = GameObject.Find("MenuControllers").GetComponent<InventoryMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		ic = GameObject.Find("ItemControllers").GetComponent<ItemController>();
		gameObject.transform.SetParent(invPanel.transform,false);
		UpdateText();
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToUse);
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void SendToUse(){
		ic.SetToUse(invItemNum);
		ic.SetSender(gameObject);
		inv.ShowTargetPanel();
	}

	public void UpdateText(){
		((Text)gameObject.GetComponentsInChildren<Text>()[0]).text = Databases.items[invItemNum].itemName;
		((Text)gameObject.GetComponentsInChildren<Text>()[1]).text = Databases.items[invItemNum].description;
		((Text)gameObject.GetComponentsInChildren<Text>()[2]).text = party.playerInventoryCount[invItemNum].ToString();
	}
	public void DestroySelf(){
		Destroy(gameObject);
	}
}
