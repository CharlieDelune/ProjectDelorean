using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour {

	public Action action;
	private AttackDefendController attackDefendController;
	private BattleItemController itemController;
	private BattleAbilityController abilityController;
	public UnitStats target;

	// Use this for initialization
	void Start () {
		attackDefendController = GameObject.Find("BattleControllers").GetComponent<AttackDefendController>();
		itemController = GameObject.Find("BattleControllers").GetComponent<BattleItemController>();
		abilityController = GameObject.Find("BattleControllers").GetComponent<BattleAbilityController>();
		target = null;
		action = Action.Null;
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null && action != Action.Null){
			TakeAction();
			action = Action.Null;
			target = null;
		}
	}

	private void TakeAction(){
		switch(action){
			case Action.Attack:
				attackDefendController.Attack(target);
				break;
			case Action.Defend:
				attackDefendController.Defend();
				break;
			case Action.Item:
				itemController.UseItem(target);
				break;
			case Action.Ability:
				abilityController.UseAbility(target);
				break;
			default:
				break;
		}
	}

	public void SetAction (Action a){
		action = a;
	}
	public void SetTarget(UnitStats unit){
		target = unit;
	}
}
