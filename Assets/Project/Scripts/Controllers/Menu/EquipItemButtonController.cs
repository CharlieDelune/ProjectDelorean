using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemButtonController : MonoBehaviour {

	private StatusMenuController stat;
	private PartyController party;
	private EquipController eq;
	public GameObject invPanel;
	public int invItemNum;
	public Text invText;

	// Use this for initialization
	void Start () {
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		eq = GameObject.Find("ItemControllers").GetComponent<EquipController>();
		gameObject.transform.SetParent(invPanel.transform,false);
		UpdateText();
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToUse);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Button>().interactable == false){
			invText.GetComponent<Text>().color = new Color(0.4f,0.4f,0.4f,1.0f);
		}
		else{
			invText.GetComponent<Text>().color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
	}

	private void SendToUse(){
		eq.SetToUse(invItemNum);
		stat.HideChangeEquipMenu();
	}

	public void UpdateText(){
		((Text)gameObject.GetComponentInChildren<Text>()).text = Databases.items[invItemNum].itemName + " x" + party.playerInventoryCount[invItemNum];
	}
}
