using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldNight2EnterTrigger : AbstractEventController {
	public GameObject newBlocker;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		PlayAnimationPersistent(player.gameObject,"IdleLeft");
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left));
		yield return StartCoroutine(ShowDialogue("Alright, I made it out. Now, to fill the team in.","Mason",masonHead));
		SetObjectActive(newBlocker,true);
		EndEventCoroutine();
	}
}
