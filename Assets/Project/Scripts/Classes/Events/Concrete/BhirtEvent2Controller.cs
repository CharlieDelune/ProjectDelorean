using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhirtEvent2Controller : AbstractEventController {
	public Sprite bhirtHead;
	public Sprite drake1head;
	public Sprite drake2head;
	public GameObject bhirtObject;
	public GameObject drake1Object;
	public GameObject drake2Object;
	public GameObject nextEventObject;
	public GameObject enemyEncounter;
	public GameObject arena;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		PlayAnimationPersistent(bhirtObject,"IdleRight");
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		PlayAnimationPersistent(drake1Object,"IdleLeft");
		PlayAnimationPersistent(drake2Object,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("We just want to get by - what's going on here?","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("Aw, look at its little scales!","Yssae",drake1head));
		yield return StartCoroutine(ShowDialogue("Tiny drake wings, tiny lizard tail!","Miene",drake2head));
		yield return StartCoroutine(ShowDialogue("Tiny half-breed!","Yssae",drake1head));
		yield return StartCoroutine(ShowDialogue("Oh, we're going to do it this way? You know how many drakes - how many lizards - I've had to \"prove myself\" to? You won't be the first corpse I leave behind.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("Whoa, Bhirt, is that really necessary?","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Violence is the only language brutes like these understand. I'm more than happy to oblige them.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("Shiny! We'll deal with you first, then your friend!","Yssae",drake1head));
		yield return StartCoroutine(ShowDialogue("I can't stand the taste of human meat, but I'll suffer through it! Ha!","Miene",drake2head));
		SetPartyMemberAvailable(party.GetUnitStats("Bhirt"),true);
		StartBattle(enemyEncounter,arena,gameObject,nextEventObject);
	}
}
