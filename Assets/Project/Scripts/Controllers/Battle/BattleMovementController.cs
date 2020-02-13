using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMovementController : MonoBehaviour {
	private BattleFlowController flow;
	private BattleUIController ui;

	// Use this for initialization
	void Start () {
		flow = gameObject.GetComponent<BattleFlowController>();
		ui = gameObject.GetComponent<BattleUIController>();
	}
	void Update(){
	}
	public void MoveCharacterToLocation(GameObject targetLocation){
		ui.HideMoveHolder();
		flow.currentUnit.battleLocation = targetLocation.name;
		Facing f = flow.currentUnit.facing;
		flow.currentUnit.battleAnimator.transform.SetParent(targetLocation.transform);
		flow.currentUnit.battleAnimator.transform.localPosition = new Vector3(0.0f,flow.currentUnit.battleAnimator.transform.position.y,0.0f);
		if(targetLocation.name.Contains(f.ToString())){
			if(f == Facing.Right){
				flow.currentUnit.facing = Facing.Left;
				flow.currentUnit.battleAnimator.GetComponent<SpriteRenderer>().flipX = false;
			}
			else if (f == Facing.Left){
				flow.currentUnit.facing = Facing.Right;
				flow.currentUnit.battleAnimator.GetComponent<SpriteRenderer>().flipX = true;
			}
		}
		flow.SetUpNextTurn(2);
	}
}