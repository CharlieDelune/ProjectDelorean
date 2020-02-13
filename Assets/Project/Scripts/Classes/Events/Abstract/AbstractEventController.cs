using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AbstractEventController : MonoBehaviour {
	public Sprite masonHead;
	public bool isInteractable;
	public PlayerBehavior player;
	public PartyController party;
	public UIController ui;
	private string previousAnimation;
	private Camera mainCamera;
	private Vector3 previousCameraPosition;
	private int currentscale = 1;
	private Coroutine waiting;
	private bool collided = false;

	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		ui = GameObject.Find("UIController").GetComponent<UIController>();
		mainCamera = Camera.main;
	}
	void Update(){
	}
	public void StartEventCoroutine(){
		player.allowingInput = false;
		player.inCutscene = true;
		StartCoroutine(FindCameraAndThenStart());
	}
	public IEnumerator FindCameraAndThenStart(){
		yield return StartCoroutine(FindCamera());
		StartCoroutine(EventCoroutine());
	}
	public abstract IEnumerator EventCoroutine();
	public void EndEventCoroutine(){
		player.allowingInput = true;
		player.inCutscene = false;
		Destroy(gameObject);
	}
	public void EndEventCoroutineNoDestroy(){
		player.allowingInput = true;
		player.inCutscene = false;
		gameObject.GetComponent<Collider>().enabled = true;
	}
	void OnTriggerEnter(Collider other) {
		if(!isInteractable){
			if(other.GetType() == typeof(BoxCollider)){
				if(other.gameObject.name == "Player"){
					GetComponent<Collider>().enabled = false;
					StartEventCoroutine();
				}
			}
		}
		else{
			if(other.GetType() == typeof(CapsuleCollider)){
				if(other.gameObject.name == "Player"){
					collided = true;
					StartCoroutine(WaitForPlayerInput());
				}
			}
		}
	}
	void OnTriggerExit(Collider other) {
		if(isInteractable){
			if(other.GetType() == typeof(BoxCollider) || other.GetType() == typeof(CapsuleCollider)){
				if(other.gameObject.name == "Player"){
					collided = false;
				}
			}
		}
	}
	public IEnumerator WaitForPlayerInput(){
		while(collided){
			if(Input.GetKeyDown("enter") || Input.GetKeyDown("return")){
				collided = false;
				StartEventCoroutine();
				break;
			}
			yield return null;
		}
	}
	public IEnumerator ShowDialogue(string text,string title = default(string),Sprite head = default(Sprite)){
		ui.ShowTextHolder();
		ui.SetDialogueText(text);
		if(title != "" && title != null){
			ui.SetDialogueTitle(title);
		}
		else{
			ui.SetDialogueTitle("");
		}
		if(head != null){
			ui.SetDialogueHeadSprite(head);
		}
		else{
			ui.SetDialogueHeadSprite(null);
		}
		yield return new WaitForSeconds(0.5f);
		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ui.HideTextHolder();
				yield break;
			}
			yield return null;
		}
	}
	public IEnumerator MoveObject(GameObject obj,Direction d){
		Vector3 moveTo = new Vector3();
		Vector3 finalMoveVector = new Vector3();
		Animator animator = obj.GetComponent<Animator>();
		switch(d){
			case Direction.Up:
				moveTo = new Vector3(0.0f, 0.0f,0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.forward;
				break;
			case Direction.Down:
				moveTo = new Vector3(0.0f, 0.0f,-0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.back;
				break;
			case Direction.Left:
				moveTo = new Vector3(-0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.left;
				break;
			case Direction.Right:
				moveTo = new Vector3(0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.right;
				break;
			default:
				Debug.Log("How did you even get here?");
				break;
		}
		float distanceTraveled = 0;
		animator.Play("Walk" + d.ToString());
		while(distanceTraveled<0.485f){
			obj.transform.position += moveTo;
			distanceTraveled += (0.375f * (Time.deltaTime*3));
			if(obj.name == "Player"){
				player.interactor.center = new Vector3(0.0f,0.0f,-0.25f);
			}
			yield return new WaitForEndOfFrame();
		}
		obj.transform.position = new Vector3(Databases.RoundToNearest(obj.transform.position.x,0.5f),obj.transform.position.y,Databases.RoundToNearest(obj.transform.position.z,0.5f));
		animator.Play("Idle" + d.ToString());
		yield return new WaitForEndOfFrame();
	}
	public IEnumerator MoveObjectNoAnimation(GameObject obj,Direction d){
		Vector3 moveTo = new Vector3();
		Vector3 finalMoveVector = new Vector3();
		switch(d){
			case Direction.Up:
				moveTo = new Vector3(0.0f, 0.0f,0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.forward;
				break;
			case Direction.Down:
				moveTo = new Vector3(0.0f, 0.0f,-0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.back;
				break;
			case Direction.Left:
				moveTo = new Vector3(-0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.left;
				break;
			case Direction.Right:
				moveTo = new Vector3(0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.right;
				break;
			default:
				Debug.Log("How did you even get here?");
				break;
		}
		float distanceTraveled = 0;
		while(distanceTraveled<0.485f){
			obj.transform.position += moveTo;
			distanceTraveled += (0.375f * (Time.deltaTime*3));
			if(obj.name == "Player"){
				player.interactor.center = new Vector3(0.0f,0.0f,-0.25f);
			}
			yield return new WaitForEndOfFrame();
		}
		obj.transform.position = new Vector3(Databases.RoundToNearest(obj.transform.position.x,0.5f),obj.transform.position.y,Databases.RoundToNearest(obj.transform.position.z,0.5f));
		yield return new WaitForEndOfFrame();
	}
	public IEnumerator MoveObject(GameObject obj,Direction d,int distance){
		Vector3 moveTo = new Vector3();
		Vector3 finalMoveVector = new Vector3();
		Animator animator = obj.GetComponent<Animator>();
		switch(d){
			case Direction.Up:
				moveTo = new Vector3(0.0f, 0.0f,0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.forward;
				break;
			case Direction.Down:
				moveTo = new Vector3(0.0f, 0.0f,-0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.back;
				break;
			case Direction.Left:
				moveTo = new Vector3(-0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.left;
				break;
			case Direction.Right:
				moveTo = new Vector3(0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.right;
				break;
			default:
				Debug.Log("How did you even get here?");
				break;
		}
		float distanceTraveled = 0;
		animator.Play("Walk" + d.ToString());
		while(distanceTraveled<(distance-.15)){
			obj.transform.position += moveTo;
			distanceTraveled += (0.375f * (Time.deltaTime*3));
			if(obj.name == "Player"){
				player.interactor.center = new Vector3(0.0f,0.0f,-0.25f);
			}
			yield return new WaitForEndOfFrame();
		}
		obj.transform.position = new Vector3(Databases.RoundToNearest(obj.transform.position.x,0.5f),obj.transform.position.y,Databases.RoundToNearest(obj.transform.position.z,0.5f));
		animator.Play("Idle" + d.ToString());
		yield return new WaitForEndOfFrame();
	}
	public IEnumerator MoveObjectNoAnimation(GameObject obj,Direction d,int distance){
		Vector3 moveTo = new Vector3();
		Vector3 finalMoveVector = new Vector3();
		switch(d){
			case Direction.Up:
				moveTo = new Vector3(0.0f, 0.0f,0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.forward;
				break;
			case Direction.Down:
				moveTo = new Vector3(0.0f, 0.0f,-0.375f*(Time.deltaTime*3));
				finalMoveVector = Vector3.back;
				break;
			case Direction.Left:
				moveTo = new Vector3(-0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.left;
				break;
			case Direction.Right:
				moveTo = new Vector3(0.375f*(Time.deltaTime*3), 0.0f,0.0f);
				finalMoveVector = Vector3.right;
				break;
			default:
				Debug.Log("How did you even get here?");
				break;
		}
		float distanceTraveled = 0;
		while(distanceTraveled<(distance-.15)){
			obj.transform.position += moveTo;
			distanceTraveled += (0.375f * (Time.deltaTime*3));
			if(obj.name == "Player"){
				player.interactor.center = new Vector3(0.0f,0.0f,-0.25f);
			}
			yield return new WaitForEndOfFrame();
		}
		obj.transform.position = new Vector3(Databases.RoundToNearest(obj.transform.position.x,0.5f),obj.transform.position.y,Databases.RoundToNearest(obj.transform.position.z,0.5f));
		yield return new WaitForEndOfFrame();
	}
	public IEnumerator PlayAnimation(GameObject target,string toPlay){
		Animator targetAnimator = target.GetComponent<Animator>();
		previousAnimation = targetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
		targetAnimator.Play(toPlay);
		yield return new WaitForSeconds(targetAnimator.runtimeAnimatorController.animationClips[0].length);
		targetAnimator.Play(previousAnimation);
	}
	public IEnumerator PlayAnimation(GameObject[] targets,string toPlay){
		foreach(GameObject target in targets){
			Animator targetAnimator = target.GetComponent<Animator>();
			previousAnimation = targetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
			targetAnimator.Play(toPlay);
			yield return new WaitForSeconds(targetAnimator.runtimeAnimatorController.animationClips[0].length);
			targetAnimator.Play(previousAnimation);
		}
	}
	public void PlayAnimationPersistent(GameObject target,string toPlay){
		Animator targetAnimator = target.GetComponent<Animator>();
		previousAnimation = targetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
		targetAnimator.Play(toPlay);
	}
	public void PlayAnimationPersistent(GameObject[] targets,string toPlay){
		foreach(GameObject target in targets){
			Animator targetAnimator = target.GetComponent<Animator>();
			previousAnimation = targetAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
			targetAnimator.Play(toPlay);
		}
	}
	public IEnumerator MoveCamera(Vector3 newPosition, float moveTime){
		float t = 0f;
		Vector3 startPosition = mainCamera.transform.localPosition;
		previousCameraPosition = startPosition;
		Vector3 endPosition = newPosition;
		while(t < 1f){
			t+= Time.deltaTime/moveTime;
			if(t>1f){t = 1f;}

			Vector3 lPosition = Vector3.Lerp(startPosition,endPosition,t);

			mainCamera.transform.localPosition = new Vector3(lPosition.x,lPosition.y,lPosition.z);
			yield return null;
		}
		yield return null;
	}
	public IEnumerator MoveCameraBack(float moveTime){
		float t = 0f;
		Vector3 startPosition = mainCamera.transform.localPosition;
		Vector3 endPosition = previousCameraPosition;
		while(t < 1f){
			t+= Time.deltaTime/moveTime;
			if(t>1f){t = 1f;}

			Vector3 lPosition = Vector3.Lerp(startPosition,endPosition,t);

			mainCamera.transform.localPosition = new Vector3(lPosition.x,lPosition.y,lPosition.z);
			yield return null;
		}
		yield return null;
	}
	public IEnumerator ResetCamera(float moveTime){
		yield return StartCoroutine(MoveCamera(new Vector3(0.0f,2.5f,-5.842f),moveTime));
		yield return null;
	}
	public GameObject SpawnObject(GameObject toSpawn,Vector3 position){
		GameObject instObject = Instantiate(toSpawn);
		instObject.transform.position = position;
		return instObject;
	}
	public void DeleteObject(GameObject toDelete){
		Destroy(toDelete);
	}
	public void DeleteObject(GameObject[] toDeleteArray){
		foreach(GameObject toDelete in toDeleteArray){
			Destroy(toDelete);
		}
	}
	public void SetPlayerFlag(string flag, bool value){
		party.flags[flag] = value;
	}
	public void StartBattle(GameObject encounterPrefab,GameObject arenaPrefab,GameObject eventDelete, GameObject nextEvent){
		SceneMgmt.SetToFightEvent(encounterPrefab,arenaPrefab,eventDelete,nextEvent,SceneManager.GetActiveScene().name,GameObject.Find("Player").transform.position);
	}
	public void SetObjectActive(GameObject o, bool toSet){
		o.SetActive(toSet);
	}
	public void SetPartyMemberAvailable(UnitStats unit, bool toSet){
		unit.available = toSet;
	}
	public void SetPartyMemberAvailable(UnitStats[] units, bool toSet){
		foreach(UnitStats unit in units){
			unit.available = toSet;
		}
	}
	public IEnumerator FindCamera(){
		while(mainCamera == null){
			if(GameObject.Find("Main Camera") != null){
				mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
			}
			yield return null;
		}
	}
	public void FlipSprite(GameObject g){
		Vector3 scale = g.transform.localScale;
		scale.x *= -currentscale;
		currentscale = -currentscale;
		g.transform.localScale = scale;
	}
	public void SetObjectPosition(GameObject g, Vector3 position){
		g.transform.localPosition = position;
	}
	public void SetObjectPosition(GameObject[] gs, Vector3 position){
		foreach(GameObject g in gs){
			g.transform.localPosition = position;
		}
	}
	public void SetPlayerPosition(Vector3 position){
		player.gameObject.transform.position = position;
	}
	public IEnumerator FadeIn(){
		yield return StartCoroutine(ui.FadeFromBlackCoroutine());
	}
	public IEnumerator FadeOut(){
		yield return StartCoroutine(ui.FadeToBlackCoroutine());
	}
	public void ChangeLevel(string name){
		SceneMgmt.LoadScene(name);
	}
}
