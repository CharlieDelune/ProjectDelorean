using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleFlowController : MonoBehaviour {

	public List<UnitStats> units;
	public PartyController party;
	private BattleUIController ui;
	public GameObject enemyEncounterObject;
	public GameObject arenaPrefab;
	public EnemyPartyController enemies;
	public UnitStats currentUnit;
	public List<UnitStats> friendlyUnits;
	public List<UnitStats> enemyUnits;

	// Use this for initialization
	void Start () {
		units = new List<UnitStats> ();
		friendlyUnits = new List<UnitStats>();
		enemyUnits = new List<UnitStats>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		enemyEncounterObject = Instantiate(SceneMgmt.toFight);
		enemyEncounterObject.transform.SetParent(GameObject.Find("BattleHolder").transform);
		enemies = enemyEncounterObject.GetComponent<EnemyPartyController>();
		ui.SetPartyAndEnemies(party,enemies);
		arenaPrefab = Instantiate(SceneMgmt.arena);
		arenaPrefab.transform.SetParent(GameObject.Find("BattleHolder").transform);
		int members = 0;
		for(int i = 0; i < party.playerParty.Length; i++){
			if(party.playerParty[i] != null && members < 4 && party.playerParty[i].GetComponent<UnitStats>().available){
				UnitStats member = party.playerParty[i].GetComponent<UnitStats>();
				if(!member.IsDead()){
					units.Add(member);
				}
				friendlyUnits.Add(member);
				members++;
				ui.CreatePartyButton(i,members);
			}
		}
		for(int i = 0; i < enemies.enemyParty.Length; i++){
			if(enemies.enemyParty[i] != null && i < 4){
				UnitStats enemy = enemies.enemyParty[i].GetComponent<UnitStats>();
				units.Add(enemy);
				enemyUnits.Add(enemy);
				ui.CreateEnemyButton(i);
			}
		}
		StartCoroutine(WaitAndThenSort());
	}
	public IEnumerator WaitAndThenSort(){
		yield return 0;
		for(int i = units.Count - 1; i >= 0; i--){
			units[i].turnCounter -= units[0].turnCounter;
		}
		yield return 0;
		NextTurn();
	}

	void Update(){
	}

	public void NextTurn (){
		bool won = true;
		bool lost = true;
		foreach(UnitStats unit in units){
			if(unit.unitType == UnitType.Friendly){
				lost = false;
			}
			if(unit.unitType == UnitType.Enemy){
				won = false;
			}
		}
		if(won){
			ui.ShowVictoryScreen();
		}
		if(lost){
			SceneMgmt.LoseFight();
		}
		if(!won && !lost){
			units.Sort();
			if(units[0].turnCounter == units[1].turnCounter){
				if(units[1].modAgility > units[0].modAgility){
					UnitStats temp = units[0];
					units[0] = units[1];
					units[1] = temp;
				}
				else if(units[1].modAgility == units[0].modAgility){
					if(units[1].baseAgility > units[0].baseAgility){
						UnitStats temp = units[0];
						units[0] = units[1];
						units[1] = temp;
					}
					else if(units[1].baseAgility == units[0].baseAgility){
						if(units[1].unitType == UnitType.Friendly && units[0].unitType == UnitType.Enemy){
							UnitStats temp = units[0];
							units[0] = units[1];
							units[1] = temp;
						}
					}
				}
			}
			ui.UpdateTurnOrder();
			/*
			//Check if beat all enemies
			GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag ("EnemyUnit");
			if (remainingEnemyUnits.Length == 0) {
				//enemyEncounter.GetComponent<CollectReward> ().GetReward ();
				SceneManager.LoadScene ("Town");
			}

			//Check if game over
			GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
			if (remainingPlayerUnits.Length == 0) {
				SceneManager.LoadScene ("Title");
			}
			*/

			currentUnit = units [0];
			units.Remove (currentUnit);
			ui.UpdateActionButtons(currentUnit);
			if(currentUnit.gameObject.tag == "EnemyUnit"){
				currentUnit.enemyAI.TakeAction();
			}
			/*
			if (!currentUnitStats.IsDead ()) {
				GameObject currentUnit = currentUnitStats.gameObject;

				currentUnitStats.CalculateTurnCounter (1);
				units.Add (currentUnitStats);
				units.Sort ();
				//ui.SetTurnOrderImages(units.ToArray());

				if (currentUnit.tag == "PlayerUnit") {
					Debug.Log("Player turn!");
					//ui.DisplayActions(currentUnitStats);
					WaitThenNextTurn();
					//GameObject.Find("PlayerParty").GetComponent<SelectUnit> ().SelectCurrentUnit (currentUnit.gameObject);
				}
				else {
					//currentUnit.GetComponent<EnemyUnitAction> ().Act ();
					Debug.Log("Enemy turn!");
					//ui.HideActions();
					WaitThenNextTurn();
				}
			}
			else {
				NextTurn ();
			}
			*/
		}
	}
	
	private IEnumerator WaitThenNextTurnRoutine(){
		yield return new WaitForSeconds (1.0f);
		NextTurn ();
	}
	public void SetUpNextTurn(int rank){
		currentUnit.CalculateTurnCounter(rank);
		if(!currentUnit.IsDead()){
			units.Add(currentUnit);
		}
		if(units.Count > 1){
			for(int i = units.Count - 1; i >= 0; i--){
				units[i].turnCounter -= units[0].turnCounter;
			}
		}
		ui.UpdatePartyButtonText();
		ui.DisableTargetButtons();
		ui.EnableMainButtons();
		NextTurn();
	}
}
