using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBlockerEvent : AbstractEventController {
	public Sprite head;
	public string dialogue;
	public string title;
	public Direction direction;
	public int distance;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue(dialogue, title, head));
		PlayAnimationPersistent(player.gameObject,"Idle" + direction);
		yield return StartCoroutine(MoveObject(player.gameObject,direction,distance));
		EndEventCoroutineNoDestroy();
	}
}
