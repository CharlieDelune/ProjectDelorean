using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartEventController : AbstractEventController {
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue("CONTEXT: You are playing as Mason, a young boy with magical powers that allow him to control the flow of time ... sort of."));
		yield return StartCoroutine(ShowDialogue("He's still learning, so he can't use the powers at will, but he has been able to use them to help people out here and there."));
		yield return StartCoroutine(ShowDialogue("Because of his abilities (Time Magic has been lost for centuries), he's amassed a small crew of people willing to follow him around."));
		yield return StartCoroutine(ShowDialogue("On his way to a shrine built to ancient time mages, he's brought his party to a nearby city to spend the night..."));
		yield return StartCoroutine(ShowDialogue("In this build, your main character (Mason) has had his stats adjusted so that he one-shots all enemies and has nearly infinite health. This is because the enemy stats and enemy AI weren't properly tuned in time."));
		EndEventCoroutine();
	}
}
