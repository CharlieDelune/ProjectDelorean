using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public Direction currentDirection;
    public Vector3 currentVector;

    private Vector3 input, startPosition,endPosition;

    public Animator animator;

    public CapsuleCollider interactor;
    private MainMenuController main;

    public float speed = 1f;

    public GameObject interactTrigger;

    public bool allowingInput;
    private float t;
    public RandomBattleZoneController dangerZone;
    public int battleBuffer = 0;
    public bool inCutscene;
    public float bulletTime;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        inCutscene = false;
        allowingInput = true;
        animator = GetComponent<Animator>();
        interactor = GetComponent<CapsuleCollider>();
        main = GameObject.Find("MenuControllers").GetComponent<MainMenuController>();
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = bulletTime;
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(!inCutscene){
                main.SetPause();
            }
        }

        if(allowingInput && !inCutscene){
            input = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            if(Mathf.Abs(input.x)>Mathf.Abs(input.z)){
                input.z = 0;
            }
            else{
                input.x = 0;
            }
            if(input != Vector3.zero){
                if(input.x < 0){
                    currentDirection = Direction.Left;
                    currentVector = Vector3.left;
                }
                if(input.x > 0){
                    currentDirection = Direction.Right;
                    currentVector = Vector3.right;
                }
                if(input.z < 0){
                    currentDirection = Direction.Down;
                    currentVector = Vector3.back;
                }
                if(input.z > 0){
                    currentDirection = Direction.Up;
                    currentVector = Vector3.forward;
                }
                //StartCoroutine(Move(transform));
                StartCoroutine(Move(transform,currentVector,currentDirection));
            }
            if(Input.GetKeyDown("enter") || Input.GetKeyDown("return")){
                if(interactTrigger != null && interactTrigger.GetComponent<Interactable>() != null){
                    interactTrigger.GetComponent<Interactable>().DoInteraction();
                }
            }
        }

    }

    private void SetMenus(){
        main = GameObject.Find("MenuControllers").GetComponent<MainMenuController>();
    }

    private void OnTriggerEnter(Collider other){
        Interactable iOther = other.gameObject.GetComponent<Interactable>();
        if(iOther != null && interactTrigger == null){
            interactTrigger = iOther.gameObject;
        }
    }
    private void OnTriggerExit(Collider other){
        if(interactTrigger == other.gameObject){
            interactTrigger = null;
        }
    }

    public void SetAllowingInput(bool toAllow){
        allowingInput = toAllow;
    }
    public IEnumerator Move(Transform entity,Vector3 v, Direction d){
        //float time = 1.0f;
        if(!checkRayHit(ConvertToVectorDirection(currentDirection))){
            allowingInput = false;
            animator.Play("Walk" + d.ToString());
            Vector3 startPos = gameObject.transform.localPosition;
            Vector3 endPos = startPos + (v * 0.5f);
            interactor.center = (v * 0.3f);
            float i = 0.0f;
            float rate = 1.0f*speed;
            while (i < 1.0f) {
                i += Time.deltaTime * rate;
                Vector3 lerpPosition = Vector3.Lerp(startPos, endPos, i);
                entity.transform.localPosition = new Vector3(lerpPosition.x,transform.position.y,lerpPosition.z);
                yield return null;
            }
            allowingInput = true;
            if(!inCutscene){
                Vector3 holdingInput = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
                if(holdingInput.x == 0 && holdingInput.z == 0){
                    animator.Play("Idle" + currentDirection.ToString());
                }
                transform.position = new Vector3(Databases.RoundToNearest(transform.position.x,0.5f),transform.position.y,Databases.RoundToNearest(transform.position.z,0.5f));
                allowingInput = true;
                yield return null;
                if(dangerZone != null && battleBuffer == 0){
                    dangerZone.BattleChanceChecker();
                }
                if(battleBuffer > 0){
                    battleBuffer--;
                }
                yield return 0;
            }
            else{
                animator.Play("Idle" + currentDirection.ToString());
                interactor.center = (v * 0.3f);
            }
        }
        else{
            animator.Play("Idle" + currentDirection.ToString());
            interactor.center = (v * 0.3f);
        }
    }
    public void StopMovement(){
        StopAllCoroutines();
        StopCoroutine(Move(gameObject.transform,currentVector,currentDirection));
        StopCoroutine("Move");
        animator.Play("Idle" + currentDirection.ToString());
        transform.position = new Vector3(Databases.RoundToNearest(transform.position.x,0.5f),transform.position.y,Databases.RoundToNearest(transform.position.z,0.5f));
    }
    public void AddDangerZone(RandomBattleZoneController rbzc){
        dangerZone  = rbzc;
    }
    public void RemoveDangerZone(){
        dangerZone  = null;
    }
    public bool checkRayHit(Vector3 direction){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, 0.7f))
        {
            if(hit.collider.gameObject.tag == "environment" || hit.collider.gameObject.tag == "NPC"){
                Debug.DrawRay(transform.position, direction * 0.5f, Color.yellow);
                return true;
            }
            else{
                Debug.DrawRay(transform.position, direction * 0.5f, Color.white);
                return false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * 0.5f, Color.white);
            return false;
        }
    }
    public void StartPositionCoroutine(){
        StartCoroutine(LockPosition());
    }
    public IEnumerator LockPosition(){
        bool position = true;
        while(position){
            if(gameObject.transform.localPosition != SceneMgmt.nextLocation){
                gameObject.transform.localPosition = SceneMgmt.nextLocation;
                yield return null;
            }
            else{
                position = false;
                yield return null;
            }
        }
        yield return null;
        gameObject.transform.localPosition = SceneMgmt.nextLocation;
    }
    public Vector3 ConvertToVectorDirection(Direction direction){
        switch(direction){
            case Direction.Up:
                return Vector3.forward;
            case Direction.Down:
                return Vector3.back;
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            default:
                return new Vector3(0.0f,0.0f,0.0f);
        }
    }
}