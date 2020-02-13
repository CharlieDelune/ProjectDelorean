using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleModeController : MonoBehaviour {
	private BattleFlowController flow;
	private UnitStats currentMember;

	// Use this for initialization
	void Start () {
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		currentMember = flow.currentUnit;
	}
	void Update(){
	}
	public void ToggleMode(){
		currentMember = flow.currentUnit;
		if(currentMember.charMode == Mode.Attack){
			currentMember.charMode = Mode.Defense;
		}
		else if (currentMember.charMode == Mode.Defense){
			currentMember.charMode = Mode.Attack;
		}
		else{

		}
		flow.SetUpNextTurn(1);
	}
}