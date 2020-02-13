using System;
using UnityEngine;

[System.Serializable]
public class DialogueNode {
	public string dialogueTag;
	public string dialogueName;
	public string dialogueText;
	public Sprite dialogueHead;
	public bool isInformation;
	public bool showChoice;
	public bool endBranch;
	public int newResetLocation;
	public bool initiateBattle;
	public string yesText;
	public string noText;
	public int indexOnYes;
	public int indexOnNo;
	public string animation;
	public GameObject animationTarget;
	public GameObject encounterPrefab;
	public GameObject arenaPrefab;
	public bool addItem;
	public bool removeItem;
	public string itemToAdd;
	public int itemNumberToAdd;
	public string itemToRemove;
	public int itemNumberToRemove;
	public string updateFlagName;
	public bool updateFlagValue;
	public string mustHaveFlagToContinue;
	public string mustNotHaveFlagToContinue;
	public bool togglePartyMember;
	public string partyMemberToToggle;
	public bool destroyAfterDialogue;
}
