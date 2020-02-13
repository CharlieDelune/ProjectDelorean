using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBossTriggerEvent4 : AbstractEventController {
	public Sprite baddieHead;
	public GameObject baddie;
	public GameObject mason2Object;
	public GameObject nextEventObject;
	public GameObject originalExitBlocker;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		SetPartyMemberAvailable(party.GetUnitStats("Mason?"),false);
		party.GetUnitStats("Mason?").charName = "Future Mason";
		SetObjectPosition(player.gameObject,new Vector3(57.0f,0.35f,-16.5f));
		SetObjectPosition(mason2Object, new Vector3(57.5f,1.35f,-26f));
		yield return StartCoroutine(MoveCamera(new Vector3(2.5f,2.5f,-5.842f),2.0f));
		PlayAnimationPersistent(player.gameObject,"BattleIdleRight");
		PlayAnimationPersistent(mason2Object,"BattleIdleRight");
		yield return StartCoroutine(ShowDialogue("What is the meaning of this!? Why are there two of you now!?", "Saddarr", baddieHead));
		yield return StartCoroutine(ShowDialogue("I'm gonna have to admit, I'm pretty confused about that myself...", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Can you just be cool for a second? I'm trying to save your ass.", "Mason?", masonHead));
		yield return StartCoroutine(ShowDialogue("Oh-ho-ho-ho, I get it now. Apologies for taking so long to catch up, I am quite new to this.", "Saddarr", baddieHead));
		yield return StartCoroutine(ShowDialogue("Yeah, totally, you're way behind ... and now that we're all on the same page, someone can explain it for the people who are lost.", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("I'm you, Mason. From the future.", "Future Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Which tells me everything I need to know: I win. You ran back here, tail between your legs, to try and save yourself.", "Saddarr", baddieHead));
		yield return StartCoroutine(ShowDialogue("I mean, actually it's more of a bootstrap kind of thing...", "Future Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("No matter, now I can dispatch both of you at once.","Saddarr",baddieHead));
		PlayAnimationPersistent(baddie,"BattleIdleLeft");
		yield return StartCoroutine(ResetCamera(2.0f));
		yield return StartCoroutine(ShowDialogue("Okay, time for you to go, buddy.", "Future Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Wait, what?", "Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("Yeah, don't worry about it. I can hold him off on my own. You've got a much more important mission now: Go get your friends, go back to before all of this started, stop it at the source.", "Future Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("That's it!? That's all you're giving me!? I think I need a little more to go on here!", "Mason", masonHead));
		yield return StartCoroutine(MoveObject(mason2Object,Direction.Down));
		StartCoroutine(PlayAnimation(mason2Object,"ThrustLeft"));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine(MoveObjectNoAnimation(player.gameObject,Direction.Left,3));
		StartCoroutine(PlayAnimation(mason2Object,"BattleIdleRight"));
		yield return StartCoroutine(ShowDialogue("Just go! You'll figure it out as you go!", "Future Mason", masonHead));
		yield return StartCoroutine(ShowDialogue("There'd better be some kind of explanation waiting for me at the end of this...", "Mason", masonHead));
		yield return StartCoroutine(MoveObject(player.gameObject,Direction.Left,3));
		SetObjectActive(nextEventObject,true);
		GameObject.Destroy(originalExitBlocker);
		yield return StartCoroutine(ShowDialogue("NOTE FROM THE DEVELOPER: There's supposed to be a boss fight here and then the layout of the level is suppsoed to change, but to streamline development of the story, it's been cut for time."));
		EndEventCoroutine();
	}
}
