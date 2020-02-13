using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	private GameObject player;
	private UIController uI;

	// Use this for initialization
	void Start () {
		Physics.gravity = new Vector3(0,0,0);
		SceneMgmt.FindPlayer();
		uI = GameObject.Find("UIController").GetComponent<UIController>();
		uI.fadeToBlack.gameObject.SetActive(false);
		if(SceneMgmt.toFight != null){
			StartCoroutine(LoadFightScene());
		}
		else if(SceneMgmt.toLoad == "Title"){
			SceneMgmt.toLoad = null;
			GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
			foreach (GameObject o in objects) {
				Destroy(o.gameObject);
			}
			SceneManager.LoadScene("Title");
		}
		else{
			StartCoroutine(LoadNewScene());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadNewScene(){
		AsyncOperation async = SceneManager.LoadSceneAsync(SceneMgmt.toLoad,LoadSceneMode.Additive);
		Scene s = SceneManager.GetSceneByName(SceneMgmt.toLoad);
		SceneMgmt.player.SetActive(true);
		while(!async.isDone){
			yield return null;
		}
		async.allowSceneActivation = true;
		SceneManager.SetActiveScene(s);
		uI.FadeIn();
		if(SceneManager.GetSceneByName("InitialLoadScreen").name != null){
			SceneManager.UnloadSceneAsync("InitialLoadScreen");	
		}
		else{
			SceneManager.UnloadSceneAsync("LoadScreen");	
		}
		SceneMgmt.player.transform.localPosition = SceneMgmt.nextLocation;
		SceneMgmt.player.GetComponent<PlayerBehavior>().allowingInput=true;
		SceneMgmt.player.GetComponent<PlayerBehavior>().inCutscene=false;
		yield return null;
	}

	static IEnumerator LoadFightScene(){
		AsyncOperation async = SceneManager.LoadSceneAsync("Battle",LoadSceneMode.Additive);
		while(!async.isDone){
			yield return null;
		}
		Scene scene = SceneManager.GetSceneByName("Battle");
		SceneManager.SetActiveScene(scene);
		SceneMgmt.previousScene = GameObject.Find("NonBattleHolder");
		SceneMgmt.previousScene.SetActive(false);
		SceneMgmt.player.SetActive(false);
		SceneManager.UnloadSceneAsync("LoadScreen");
	}
}
