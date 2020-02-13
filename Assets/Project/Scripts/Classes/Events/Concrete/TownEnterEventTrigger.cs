using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownEnterEventTrigger : AbstractEventController {
	public Sprite baddie1Head;
	public Sprite goodie1Head;
	public GameObject baddie1Object;
	public GameObject goodie1Object;
	public GameObject nextEventObject;
	public GameObject enemyEncounter;
	public GameObject arena;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		PlayAnimationPersistent(goodie1Object,"IdleLeft");
		PlayAnimationPersistent(baddie1Object,"BattleIdleRight");
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine(ShowDialogue("OH GODS! HELP US! SOMEBODY HELP US!", "?????"));
		PlayAnimationPersistent(player.gameObject,"IdleUp");
		yield return new WaitForSeconds(0.5f);
		PlayAnimationPersistent(player.gameObject,"IdleLeft");
		yield return new WaitForSeconds(0.5f);
		PlayAnimationPersistent(player.gameObject,"IdleDown");
		yield return new WaitForSeconds(0.5f);
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		StartCoroutine(PlayAnimation(baddie1Object,"ThrustRight"));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		StartCoroutine(PlayAnimation(baddie1Object,"ThrustRight"));
		yield return StartCoroutine(ShowDialogue("What's going on here!?", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Oh thank the gods! Please save us!", "Terrified Woman", goodie1Head));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(ShowDialogue("Stop attacking this woman!", "Mason", masonHead));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(ShowDialogue("Leave! Now!", "Angry Orc", baddie1Head));
		yield return StartCoroutine(ShowDialogue("Stop now, I'm warning you!", "Mason", masonHead));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		PlayAnimationPersistent(baddie1Object,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("Fine! Me gut you first, then lady!", "Angry Orc", baddie1Head));
		yield return StartCoroutine(PlayAnimation(baddie1Object,"ThrustLeft"));
		StartBattle(enemyEncounter,arena,gameObject,nextEventObject);
		EndEventCoroutine();
	}
}
