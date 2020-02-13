using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonController : MonoBehaviour {

	public Image fadeImage;
	public Text fadeText;
	public AudioSource music;

	[SerializeField]
	private Vector3 spawnPosition;
	[SerializeField]
	private string firstLevel;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button>().onClick.AddListener(StartGame);
	}
	
	// Update is called once per frame
	void StartGame () {
		SceneMgmt.SetLocation(spawnPosition);
		StartCoroutine(FadeToLoadAndThenStartGame());
	}
	IEnumerator FadeToLoadAndThenStartGame(){
		fadeImage.gameObject.SetActive(true);
		fadeText.gameObject.SetActive(true);
		float t = 0;
		while (t<= 1.0f){
			music.volume -= Time.deltaTime;
			t += Time.deltaTime;
			Color temp = fadeImage.color;
			temp.a += Time.deltaTime;
			fadeImage.color = temp;
			temp = fadeText.color;
			temp.a += Time.deltaTime;
			fadeText.color = temp;
			yield return null;
		}
		if(music.volume != 0){
			music.volume = 0;
		}
		yield return null;
		SceneMgmt.LoadInitial(firstLevel);
	}
}
