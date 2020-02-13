using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventoryButtonController : MonoBehaviour {
	public PartyController party;
	private BattleItemController controller;
	private BattleUIController ui;
	private ItemController ic;
	public GameObject invPanel;
	public TargetHandler target;
	public Action action;
	public int invItemNum;
	public Text invText;
	public Text invDesc;
	public Text invCount;

	// Use this for initialization
	void Start () {
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		target = GameObject.Find("BattleControllers").GetComponent<TargetHandler>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		controller = GameObject.Find("BattleControllers").GetComponent<BattleItemController>();
		gameObject.transform.SetParent(invPanel.transform,false);
		StartCoroutine("WaitAndUpdateText");
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SetAction);
		action = Action.Item;
	}
	public void UpdateText(){
		invText.text = Databases.items[invItemNum].itemName;
		invDesc.text = Databases.items[invItemNum].description;
		invCount.text = party.playerInventoryCount[invItemNum].ToString();
	}
	void SetAction(){
		target.SetAction(action);
		controller.SetItem(invItemNum);
		ui.HideInventoryPanel();
		ui.DisableMainButtons();
		ui.EnableTargetButtons();
	}
	public IEnumerator WaitAndUpdateText(){
		yield return 0;
		UpdateText();
	}
}
