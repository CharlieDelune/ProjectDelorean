using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPostBattleEventController : AbstractEventController {
	public Sprite goodiehead;
	public GameObject goodie;
	public TownBossBlockerEvent bossBlockerEvent;
	public string[] toSay = {"Thank you so much! I don't know what I'd have done if you hadn't shown up!","You saved me! I don't know how I'll ever thank you!","Thank the gods for sending you to us!","There are still others out there... Please, you have to save them too!"};
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		bossBlockerEvent.reduceToRescue();
		int r = Random.Range(0,toSay.Length);
		yield return StartCoroutine(ShowDialogue(toSay[r],null,goodiehead));
		EndEventCoroutine();
	}
}
