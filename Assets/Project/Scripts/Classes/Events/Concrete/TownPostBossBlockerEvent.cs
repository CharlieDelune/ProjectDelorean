using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPostBossBlockerEvent : AbstractEventController {

	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue("Future Me has this handled, I've got to go assmble the team.", "Mason", masonHead));
		PlayAnimationPersistent(player.gameObject,"IdleLeft");
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left,2));
		EndEventCoroutineNoDestroy();
	}
}
