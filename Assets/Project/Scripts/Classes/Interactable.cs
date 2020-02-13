using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour {

	public string[] informationText;

	public DialogueNode[] thingsToSay;

	private DialogueController dc;

	private int loc;

	private PartyController party;

	public string mustHaveFlagToExist;
	public string mustNotHaveFlagToExist;
	public Direction facing;
	private Animator anim;

	void Start(){
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		loc = 0;
		dc = GameObject.Find("DialogueController").GetComponent<DialogueController>();
		anim = gameObject.GetComponent<Animator>();
		switch(facing){
			case Direction.Up:
				anim.Play("IdleUp");
				break;
			case Direction.Down:
				anim.Play("IdleDown");
				break;
			case Direction.Left:
				anim.Play("IdleLeft");
				break;
			case Direction.Right:
				anim.Play("IdleRight");
				break;
			default:
				break;
		}
	}

	void Update(){
		if(mustNotHaveFlagToExist != null && mustNotHaveFlagToExist != ""){
			if(party.flags[mustNotHaveFlagToExist]){
				Destroy(gameObject);
			}
		}
		if(mustHaveFlagToExist != null && mustHaveFlagToExist != ""){
			if(!party.flags[mustHaveFlagToExist]){
				Destroy(gameObject);
			}
		}
	}

	public void DoInteraction(){
		Direction d = GameObject.Find("Player").GetComponent<PlayerBehavior>().currentDirection;
		switch(d){
			case Direction.Up:
				facing = Direction.Down;
				gameObject.GetComponent<Animator>().Play("IdleDown");
				break;
			case Direction.Down:
				facing = Direction.Up;
				gameObject.GetComponent<Animator>().Play("IdleUp");
				break;
			case Direction.Right:
				facing = Direction.Left;
				gameObject.GetComponent<Animator>().Play("IdleLeft");
				break;
			case Direction.Left:
				facing = Direction.Right;
				gameObject.GetComponent<Animator>().Play("IdleRight");
				break;
			default:
				break;
		}
		dc.DisplayDialogue(gameObject);
	}

	public DialogueNode GetNextNode(){
		if(loc < thingsToSay.Length){
			DialogueNode toReturn = thingsToSay[loc];
			loc++;
			return toReturn;
		}
		else{
			return null;
		}
	}

	public DialogueNode GetNextNode(int newNode){
		if(newNode< thingsToSay.Length){
			loc = newNode;
			if(loc < thingsToSay.Length){
				DialogueNode toReturn = thingsToSay[loc];
				loc++;
				return toReturn;
			}
			else{
				return null;
			}
		}
		else{
			return null;
		}
	}

	public void ResetLoc(int i){
		loc = i;
	}
}
