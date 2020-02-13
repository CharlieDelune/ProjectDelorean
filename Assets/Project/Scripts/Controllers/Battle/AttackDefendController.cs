using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDefendController : MonoBehaviour {
	public Action action;
	public BattleFlowController flow;
	public BattleUIController ui;
	public int rank;
	void Start () {
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		action = Action.Null;
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void Attack(UnitStats target){
		StartCoroutine(WaitAndThenGetHit(target));
	}
	public void Defend(){
		flow.SetUpNextTurn(3);
	}
	IEnumerator WaitAndThenGetHit(UnitStats target){
		ui.DisableMainButtons();
		ui.DisableTargetButtons();
		flow.currentUnit.battleAnimator.GetComponent<Animator>().Play(flow.currentUnit.defaultAttackAnimation + flow.currentUnit.facing.ToString());
		yield return new WaitForSeconds(0.33f);
		DealDamageToTarget(target);
		yield return new WaitForSeconds(1.0f);
		flow.SetUpNextTurn(3);
	}

	public void DealDamageToTarget(UnitStats target){
		int dmg = Mathf.RoundToInt((((flow.currentUnit.modAttack*flow.currentUnit.modAttack*flow.currentUnit.modAttack)/32)+32)*16/16);
		int defNum = Mathf.RoundToInt((((target.modDefense - 280.4f) * (target.modDefense - 280.4f)/110)+16));
		int dmg2 = (dmg * defNum) / 730;
		int finalDmg = dmg2 * (730 - (target.modDefense * 51 - (target.modDefense * target.modDefense) / 11)/10)/730;
		int modFinalDmg = finalDmg;
		if(target.charMode == Mode.Defense){
			modFinalDmg = Mathf.RoundToInt(modFinalDmg/2);
		}
		target.ReceiveDamage(modFinalDmg);
		ui.ShowDamageText(target,modFinalDmg);
		if(target.IsDead()){
			target.battleAnimator.GetComponent<Animator>().Play("Die");
			flow.units.Remove(target);
		}
		else{
			target.battleAnimator.GetComponent<Animator>().Play("Hit" + target.facing.ToString());
		}
	}
}
