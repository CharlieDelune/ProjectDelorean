using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomBattleZoneController : MonoBehaviour {
	public float encounterChance;
	public GameObject[] encounters;
	public GameObject arenaPrefab;
	private PlayerBehavior player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetType() == typeof(BoxCollider)){
			if(other.gameObject.name == "Player"){
				other.gameObject.GetComponent<PlayerBehavior>().AddDangerZone(this);
			}
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.GetType() == typeof(BoxCollider)){
			if(other.gameObject.name == "Player"){
				other.gameObject.GetComponent<PlayerBehavior>().RemoveDangerZone();
			}
		}
	}
	public void BattleChanceChecker(){
		float r = Random.value;
		if(r <= encounterChance){
			player.battleBuffer = 6;
			int r2 = Random.Range(0,encounters.Length);
			SceneMgmt.SetToFight(encounters[r2],arenaPrefab,SceneManager.GetActiveScene().name,GameObject.Find("Player").transform.position);
		}
	}
}
