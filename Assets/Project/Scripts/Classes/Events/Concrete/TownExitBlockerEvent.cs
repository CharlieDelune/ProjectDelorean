using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownExitBlockerEvent : AbstractEventController {
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue("I can't just leave, I have to help these people.", "Mason", masonHead));
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right,2));
		EndEventCoroutineNoDestroy();
	}
}
