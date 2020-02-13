using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDefendButtonController : MonoBehaviour {
	public TargetHandler target;
	public BattleFlowController flow;
	public Action action;
	public BattleUIController ui;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("BattleControllers").GetComponent<TargetHandler>();
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		action = Action.Attack;
		gameObject.GetComponent<Button>().onClick.AddListener(SetAction);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponentInChildren<Text>().text == "Attack" && action == Action.Defend){
			action = Action.Attack;
		}
		else if(gameObject.GetComponentInChildren<Text>().text == "Defend" && action == Action.Attack){
			action = Action.Defend;
		}
	}
	void SetAction(){
		target.SetAction(action);
		switch(action){
			case Action.Attack:
				ui.DisableMainButtons();
				ui.EnableTargetButtons();
				break;
			case Action.Defend:
				target.SetTarget(flow.currentUnit);
				break;
			default:
				break;
		}
	}
}
