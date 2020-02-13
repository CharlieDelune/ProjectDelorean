﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BhirtEvent3Controller : AbstractEventController {
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
		yield return StartCoroutine(MoveCamera(new Vector3(2.5f,2.5f,-5.842f),1.0f));
		PlayAnimationPersistent(drake1Object,"IdleUp");
		PlayAnimationPersistent(drake2Object,"IdleDown");
		yield return new WaitForSeconds(1.0f);
		PlayAnimationPersistent(drake1Object,"IdleLeft");
		PlayAnimationPersistent(drake2Object,"IdleLeft");
		yield return new WaitForSeconds(1.0f);
		PlayAnimationPersistent(drake1Object,"IdleUp");
		PlayAnimationPersistent(drake2Object,"IdleDown");
		yield return new WaitForSeconds(1.0f);
		PlayAnimationPersistent(drake1Object,"IdleLeft");
		PlayAnimationPersistent(drake2Object,"IdleLeft");
		yield return new WaitForSeconds(1.0f);
		PlayAnimationPersistent(drake1Object,"IdleUp");
		PlayAnimationPersistent(drake2Object,"IdleDown");
		yield return StartCoroutine(ShowDialogue("The tiny half-breed cheated!","Miene",drake2head));
		yield return StartCoroutine(ShowDialogue("Of course! It's the only explanation!","Yssae",drake1head));
		PlayAnimationPersistent(drake1Object,"IdleLeft");
		PlayAnimationPersistent(drake2Object,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("Just wait'll we get the boss involved in this!","Yssae",drake1head));
		yield return StartCoroutine(ShowDialogue("Yeah! You'll be sorry then, filthy half-breed!","Miene",drake2head));
		yield return StartCoroutine(ShowDialogue("And all of your filthy friends, too! Come on, Miene!","Yssae",drake1head));
		yield return StartCoroutine(ShowDialogue("Coming, Yssae!","Miene",drake2head));
		DeleteObject(GameObject.Find("BoundaryToDeleteAfterBhirtEvent"));
		StartCoroutine(ResetCamera(1.0f));
		StartCoroutine(MoveObject(drake2Object,Direction.Right));
		yield return StartCoroutine(MoveObject(drake1Object,Direction.Right));
		StartCoroutine(MoveObject(drake2Object,Direction.Right));
		yield return StartCoroutine(MoveObject(drake1Object,Direction.Right));
		StartCoroutine(MoveObject(drake2Object,Direction.Right));
		yield return StartCoroutine(MoveObject(drake1Object,Direction.Right));
		StartCoroutine(MoveObject(drake2Object,Direction.Right));
		yield return StartCoroutine(MoveObject(drake1Object,Direction.Right));
		StartCoroutine(MoveObject(drake2Object,Direction.Right));
		yield return StartCoroutine(MoveObject(drake1Object,Direction.Right));
		DeleteObject(drake1Object);
		DeleteObject(drake2Object);
		yield return StartCoroutine(ShowDialogue("Hey, Bhirt?","Mason",masonHead));
		PlayAnimationPersistent(bhirtObject,"IdleLeft");
		yield return StartCoroutine(ShowDialogue("I'm sorry about all that stuff they said. You know we don't feel that way about you, right?","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("They're not wrong, Mason. I am a half-breed. Part Lizard, part Drake.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("That just means the best of both worlds, right?","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Hah! Hahahaha... Oh, Mason. You're naive in the best way.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("Drakes and Lizards are both pretty insular... Being a half-breed just means being ostracized by both. My tail's too short to fit in with the Lizards, and my wings are too small to fit in with the Drakes.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("It doesn't help that I like parts of both of their cultures. And hate parts, too.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("You'll always have a place here with us, Bhirt. Maybe that doesn't make up for anything, but...","Mason",masonHead));
		yield return StartCoroutine(ShowDialogue("Yeah. It's a start.","Bhirt",bhirtHead));
		yield return StartCoroutine(ShowDialogue("Thanks, Mason.","Bhirt",bhirtHead));
		SetPartyMemberAvailable(party.GetUnitStats("Bhirt"),false);
		EndEventCoroutine();
	}
}