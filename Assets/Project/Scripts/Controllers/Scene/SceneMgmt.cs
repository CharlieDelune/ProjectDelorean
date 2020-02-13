using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneMgmt {

	public static string toLoad;
	public static GameObject toFight;
	public static GameObject arena;
	public static GameObject previousScene;
	public static Vector3 nextLocation;
	public static GameObject player;
	public static GameObject nextEvent;
	
	public static void LoadScene(string name){
		toLoad = name;
		SceneManager.LoadScene("LoadScreen");
	}
	public static void SetToFight(GameObject tf, GameObject a,string sc, Vector3 prevLoc){
		toFight = tf;
		arena = a;
		SceneManager.LoadScene("LoadScreen",LoadSceneMode.Additive);
	}
	public static void SetToFightEvent(GameObject tf, GameObject a,GameObject td, GameObject ne,string sc, Vector3 prevLoc){
		GameObject.Destroy(td);
		toFight = tf;
		arena = a;
		nextEvent = ne;
		SceneManager.LoadScene("LoadScreen",LoadSceneMode.Additive);
	}
	public static void WinFight(){
		toFight = null;
		arena = null;
		SceneManager.UnloadSceneAsync("Battle");
		previousScene.SetActive(true);
		player.SetActive(true);
		player.transform.position = new Vector3(Databases.RoundToNearest(player.transform.position.x,0.5f),player.transform.position.y,Databases.RoundToNearest(player.transform.position.z,0.5f));
		player.GetComponent<PlayerBehavior>().allowingInput = true;
		Physics.gravity = new Vector3(0,-9.8f,0);
		if(nextEvent != null){
			nextEvent.SetActive(true);
		}
		nextEvent = null;
	}
	public static void LoseFight(){
		toFight = null;
		arena = null;
		toLoad = "Title";
		SceneManager.LoadScene("LoadScreen");
	}
	public static void ResetToFight(){
		toFight = null;
	}
	public static void SetLocation(Vector3 position){
		nextLocation = position;
	}
	public static void ResetToLoad(){
		toLoad = "";
	}
	public static void LoadInitial(string name){
		toLoad = name;
		SceneManager.LoadScene("InitialLoadScreen");
	}
	public static void FindPlayer(){
		player = GameObject.Find("Player");
	}
}
