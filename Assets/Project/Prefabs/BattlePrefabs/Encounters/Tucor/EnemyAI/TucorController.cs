using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TucorController : AbstractEnemyAIController {
	public override void TakeAction(){
		currentTurn++;
		if(currentTurn == 1){
			Attack(SelectRandomEnemy());
		}
		else if(currentTurn == 2){
			Ability(0,SelectAllInFront());
		}
		else{
			Attack(SelectRandomEnemy());
		}
		/*
		else if(currentTurn > 1 && currentTurn < 6){
			if(((currentUnit.currentHealth/currentUnit.maxHealth) <= 0.33f) && enemyParty.HaveItem("HPRESTORE1")){
				Item("HPRESTORE1",SelectSelf());
			}
			else{
				Attack(SelectHighestHPEnemy());
			}
		}
		else if(currentTurn == 6){

		}
		*/
	}
}
