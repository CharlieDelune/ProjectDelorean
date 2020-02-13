using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEventController : AbstractEventController {
	public Sprite catHead;
	public GameObject catObject;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return StartCoroutine(ShowDialogue("Mrph!","Cat",catHead));
		yield return StartCoroutine(MoveObject(catObject,Direction.Right));
		yield return StartCoroutine(ShowDialogue("Cat? What's wrong?","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Hrmph! Braawwrr!","Cat",catHead));
		yield return StartCoroutine(ShowDialogue("I know you want to come with me, but we can't just bring a bear into town...","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Grr...","Cat",catHead));
		yield return StartCoroutine(ShowDialogue("Next time, buddy, I promise. I'll come right back if anything goes wrong!","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Mrph.","Cat",catHead));
		yield return StartCoroutine(MoveObject(catObject,Direction.Left));
		PlayAnimationPersistent(catObject,"IdleRight");
		EndEventCoroutine();
	}
}
