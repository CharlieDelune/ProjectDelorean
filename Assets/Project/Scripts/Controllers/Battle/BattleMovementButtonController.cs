using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMovementButtonController : MonoBehaviour {

	public GameObject associatedLocation;
	public BattleMovementController move;

	// Use this for initialization
	void Start () {
		move = GameObject.Find("BattleControllers").GetComponent<BattleMovementController>();
		gameObject.GetComponent<Button>().onClick.AddListener(moveToLocation);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void moveToLocation(){
		move.MoveCharacterToLocation(associatedLocation);
	}
}
