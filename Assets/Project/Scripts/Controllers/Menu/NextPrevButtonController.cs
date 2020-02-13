using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPrevButtonController : MonoBehaviour {
	private StatusMenuController stat;
	private GameObject[] party;
	public int currentIndex;
	private int nextIndex;
	private int previousIndex;

	void Start(){
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		currentIndex = 0;
		GetLocation();
	}

	void Update(){
	}
	
	public void GoToNext(){
		stat.SetStatusTarget(nextIndex);
		currentIndex = nextIndex;
		stat.UpdateStatusText();
		//GetLocation();
	}

	public void GoToPrevious(){
		stat.SetStatusTarget(previousIndex);
		currentIndex = previousIndex;
		stat.UpdateStatusText();
		//GetLocation();
	}

	public void GetLocation(){
		if(party == null){
			party = GameObject.Find("PlayerParty").GetComponent<PartyController>().playerParty;
		}
		for(int i = currentIndex + 1; i <= party.Length; i++){
			if(i == party.Length){
				i = 0;
			}
			if(party[i].GetComponent<UnitStats>().available){
				nextIndex = i;
				break;
			}
		}
		for(int i = currentIndex-1; i >= -1; i--){
			if(i == -1){
				i = party.Length -1;
			}
			if(party[i].GetComponent<UnitStats>().available){
				previousIndex = i;
				break;
			}
		}
		//Debug.Log(currentIndex + " " + nextIndex + " " + previousIndex);
		/*
		if(nextIndex > (party.Length -1)){
			nextIndex = 0;
		}
		if(previousIndex < 0){
			previousIndex = party.Length - 1;
		}
		*/
	}
}
