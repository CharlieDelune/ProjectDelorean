using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownBattleEventController : AbstractEventController {
	public Sprite baddieHead;
	public Sprite goodiehead;
	public GameObject baddie;
	public GameObject goodie;
	public GameObject encounterPrefab;
	public GameObject arenaPrefab;
	public TownBossBlockerEvent bossBlockerEvent;
	public GameObject nextEventObject;
	public override IEnumerator EventCoroutine(){
		player.StopMovement();
		yield return new WaitForSeconds(0.5f);
		StartBattle(encounterPrefab,arenaPrefab,gameObject,nextEventObject);
		yield return null;
		EndEventCoroutine();
	}
}
