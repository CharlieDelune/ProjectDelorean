using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPartyController : MonoBehaviour {

	public int experienceGained;
	public int coinsGained;
	public string[] itemNames;
	public int[] itemCounts;
	public GameObject[] enemyParty;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	public bool HaveItem(string itemToFind){
		for(int i = 0; i < itemNames.Length; i++){
			if(itemNames[i].Equals(itemToFind)){
				if(itemCounts[i]>0){
					return true;
				}
			}
		}
		return false;
	}
}
