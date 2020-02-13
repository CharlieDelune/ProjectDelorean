using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyChecker : MonoBehaviour {
	PartyController party;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if(other.gameObject.name == "Player"){
			bool destroy = true;
			foreach(GameObject member in party.playerParty){
				UnitStats unit = member.GetComponent<UnitStats>();
				if(unit.charName != "Future Mason" && unit.charName != "Mason?"){
					if(!unit.available){
						destroy = false;
					}
				}
			}
			if(destroy){
				GameObject.Destroy(gameObject);
			}
		}
	}
}
