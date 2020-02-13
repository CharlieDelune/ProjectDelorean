using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyAIController : MonoBehaviour {
	public BattleFlowController flow;
	public BattleUIController ui;
	public AttackDefendController attackDefend;
	public BattleModeController mode;
	public BattleAbilityController ability;
	public BattleItemController item;
	public int currentTurn;
	public UnitStats currentUnit;
	public EnemyPartyController enemyParty;
	
	// Use this for initialization
	void Start () {
		currentUnit = gameObject.GetComponent<UnitStats>();
		enemyParty = GameObject.Find("EnemyParty(Clone)").GetComponent<EnemyPartyController>();
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		attackDefend = GameObject.Find("BattleControllers").GetComponent<AttackDefendController>();
		mode = GameObject.Find("BattleControllers").GetComponent<BattleModeController>();
		item = GameObject.Find("BattleControllers").GetComponent<BattleItemController>();
		ability = GameObject.Find("BattleControllers").GetComponent<BattleAbilityController>();
	}
	public abstract void TakeAction();
	public void Attack(UnitStats target){
		attackDefend.Attack(target);
	}
	public void Defend(){
		attackDefend.Defend();
	}
	public void Mode(){
		mode.ToggleMode();
	}
	public void Item(string toUse, UnitStats target){
		item.SetItem(Databases.FindItem(toUse));
		item.UseItem(target);
	}
	public void Ability(int able,UnitStats target){
		ability.SetAbility(able);
		ability.UseAbility(target);
	}
	public void Ability(int able,UnitStats[] target){
		ability.SetAbility(able);
		ability.UseAbility(target);
	}
	public UnitStats SelectRandomEnemy(){
		int index = Random.Range(0,flow.friendlyUnits.Count);
		while(flow.friendlyUnits[index].IsDead()){
			index = Random.Range(0,flow.friendlyUnits.Count);
		}
		return flow.friendlyUnits[index];
	}
	public UnitStats SelectHighestHPEnemy(){
		int index = 0;
		for(int i=1;i<flow.friendlyUnits.Count;i++){
			if(flow.friendlyUnits[i].currentHealth > flow.friendlyUnits[index].currentHealth){
				index = i;
			}
		}
		return flow.friendlyUnits[index];
	}
	public UnitStats SelectLowestHPEnemy(){
		int index = 0;
		for(int i=1;i<flow.friendlyUnits.Count;i++){
			if(flow.friendlyUnits[i].currentHealth < flow.friendlyUnits[index].currentHealth && !flow.friendlyUnits[i].IsDead()){
				index = i;
			}
		}
		return flow.friendlyUnits[index];
	}
	public UnitStats SelectNextEnemy(){
		return flow.units[0];
	}
	public UnitStats SelectSelf(){
		return flow.currentUnit;
	}
	public UnitStats[] SelectAllPlayerUnits(){
		return flow.friendlyUnits.ToArray();
	}
	public UnitStats[] SelectAllEnemyUnits(){
		return flow.enemyUnits.ToArray();
	}
	public UnitStats[] SelectAllUnits(){
		List<UnitStats> l = flow.units;
		l.Add(flow.currentUnit);
		return l.ToArray();
	}
	public UnitStats[] SelectAllButSelf(){
		return flow.units.ToArray();
	}
	public UnitStats[] SelectAllInFront(){
		List<UnitStats> l = new List<UnitStats>();
		foreach(UnitStats u in flow.friendlyUnits){
			if(!u.facing.Equals(flow.currentUnit.facing)){
				l.Add(u);
			}
		}
		return l.ToArray();
	}
	public UnitStats[] SelectAllBehind(){
		List<UnitStats> l = new List<UnitStats>();
		foreach(UnitStats u in flow.friendlyUnits){
			if(u.facing.Equals(flow.currentUnit.facing)){
				l.Add(u);
			}
		}
		return l.ToArray();
	}
}
