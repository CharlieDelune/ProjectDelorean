using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAbilityButtonController : MonoBehaviour {
	private BattleFlowController flow;
	private BattleUIController ui;
	private TargetHandler target;
	private BattleAbilityController controller;
	public PartyController party;
	public GameObject holderPanel;
	public int abilityNum;
	public Text abiText;
	public Text abiDesc;
	public Text abiCost;
	private Action action;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("BattleControllers").GetComponent<BattleAbilityController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		target = GameObject.Find("BattleControllers").GetComponent<TargetHandler>();
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		gameObject.transform.SetParent(holderPanel.transform,false);
		StartCoroutine("WaitAndUpdateText");
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SetAction);
		action = Action.Ability;
	}
	public void UpdateText(){
		abiText.text = flow.currentUnit.charAbilities[abilityNum].abilityName;
		abiDesc.text = flow.currentUnit.charAbilities[abilityNum].abilityDesc;
		abiCost.text = flow.currentUnit.charAbilities[abilityNum].mpCost.ToString();
	}
	void SetAction(){
		target.SetAction(action);
		controller.SetAbility(abilityNum);
		ui.HideAbilityPanel();
		ui.DisableMainButtons();
		ui.EnableTargetButtons();
	}
	public IEnumerator WaitAndUpdateText(){
		yield return 0;
		UpdateText();
	}
}
