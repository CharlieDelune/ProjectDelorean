using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownEnterEvent2Trigger : AbstractEventController {
	public Sprite baddie1Head;
	public Sprite goodie1Head;
	public GameObject baddie1Object;
	public GameObject goodie1Object;
	public GameObject nextEventObject;
	public GameObject nextEventObject2;
	public GameObject enemyEncounter;
	public GameObject arena;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		DeleteObject(baddie1Object);
		SetObjectPosition(goodie1Object,new Vector3(0.0f,1.36f,0.0f));
		SetPlayerPosition(new Vector3(-1.0f,0.42f,10.0f));
		PlayAnimationPersistent(goodie1Object,"IdleLeft");
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Right));
		yield return StartCoroutine(ShowDialogue("Oh thank the gods! There are more of us! Please, you have to help! Th-They have my son!", "Terrified Woman", goodie1Head));
		yield return StartCoroutine(ShowDialogue("Of course! Get yourself to safety, I'll take care of things here!", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Thank you, young man!", "Terrified Woman", goodie1Head));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Down));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		yield return StartCoroutine(MoveObject(goodie1Object,Direction.Left));
		DeleteObject(goodie1Object);
		yield return StartCoroutine(ShowDialogue("There's not enough time to go get the team...", "Mason", masonHead));
		yield return StartCoroutine(PlayAnimation(player.gameObject,"AttackRight"));
		yield return StartCoroutine(PlayAnimation(player.gameObject,"AttackLeft"));
		yield return StartCoroutine(PlayAnimation(player.gameObject,"AttackRight"));
		PlayAnimationPersistent(player.gameObject,"IdleRight");
		yield return StartCoroutine(ShowDialogue("Right! It's up to me then! I should head into the city and take out all the orcs I come across on the way.", "Mason", masonHead));
		SetObjectActive(nextEventObject,true);
		SetObjectActive(nextEventObject2,true);
		EndEventCoroutine();
	}
}
