using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBossTriggerEvent2 : AbstractEventController {
	public Sprite baddieHead;
	public Sprite orcHead;
	public GameObject baddie;
	public GameObject orc3;
	public GameObject orc4;
	public GameObject orc5;
	public GameObject orc6;
	public GameObject orc7;
	public GameObject orc8;
	public GameObject enemyEncounter;
	public GameObject arenaObject;
	public GameObject nextEventObject;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		SetObjectPosition(player.gameObject,new Vector3(57.0f,0.35f,-16.5f));
		PlayAnimationPersistent(player.gameObject,"BattleIdleRight");
		PlayAnimationPersistent(baddie,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("You certainly are just as tenacious as the Mason I know! It is a shame you stand on the wrong side of this conflict. There's still time to join me and put an end to this.", "Saddarr", baddieHead));
		yield return StartCoroutine(ShowDialogue("Clearly I'm not the Mason you know! I'd never agree to a plan that involved something like this!", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Agree to it? Mason, you came up with it! You are the grand architect, and I just a tool with which you build.", "Saddarr", baddieHead));
		yield return StartCoroutine(ShowDialogue("Enough! None of what you're saying makes any sense! No wonder you've murdered all of this people: you're completely mad!", "Mason", masonHead));
		PlayAnimationPersistent(baddie,"IdleUp");
		yield return StartCoroutine(ShowDialogue("Well! That is just rude, Mason. Fine, then. Have it your way.", "Saddarr", baddieHead));
		StartCoroutine(MoveObject(orc3,Direction.Down));
		yield return StartCoroutine(MoveObject(orc4,Direction.Down));
		StartCoroutine(MoveObject(orc3,Direction.Down));
		yield return StartCoroutine(MoveObject(orc4,Direction.Down));
		StartCoroutine(PlayAnimation(orc3,"ThrustLeft"));
		yield return StartCoroutine(PlayAnimation(orc4,"ThrustLeft"));
		DeleteObject(orc3);
		DeleteObject(orc4);
		StartBattle(enemyEncounter,arenaObject,gameObject,nextEventObject);
		EndEventCoroutine();
	}
}
