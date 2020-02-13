using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBossBlockerEvent : AbstractEventController {
	private static int toRescue = 5;
	void Update(){
		if(toRescue == 0){
			Destroy(gameObject);
		}
	}
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue("I can't move on yet, there are still people in town to save.", "Mason", masonHead));
		PlayAnimationPersistent(player.gameObject,"IdleLeft");
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left,2));
		EndEventCoroutineNoDestroy();
	}
	public void reduceToRescue(){
		toRescue--;
	}
}
