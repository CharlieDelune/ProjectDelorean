using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour {

	private UIController ui;
	private Interactable owner;
	
	private bool allowingInput;

	private DialogueNode toSay;
	private PartyController party;

	void Start(){
		ui = GameObject.Find("UIController").GetComponent<UIController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		allowingInput = true;
		toSay = null;
	}

	public void DisplayDialogue(GameObject itemOwner){
		toSay = null;
		owner = itemOwner.GetComponent<Interactable>();
		//ui.SetDialogueHeadSprite(owner.headImage);
		//ui.SetDialogueTitle(owner.title);
		StartCoroutine(WaitForKeyDownStart());
	}
	IEnumerator WaitForKeyDownStart()
	{
		ui.ShowTextHolder();
		while (true)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				if(allowingInput){
					if(toSay != null && toSay.updateFlagName != null && toSay.updateFlagName != ""){
						party.flags[toSay.updateFlagName] = toSay.updateFlagValue;
					}
					if (toSay != null && toSay.initiateBattle)
                    {
                        ui.HideTextHolder();
                        owner.ResetLoc(toSay.newResetLocation);
						SceneMgmt.SetToFight(toSay.encounterPrefab,toSay.arenaPrefab,SceneManager.GetActiveScene().name,GameObject.Find("Player").transform.position);
						//SceneMgmt.LoadScene("Battle");
                        yield break;
                    }
					if(toSay != null && toSay.endBranch){
						ui.HideTextHolder();
						owner.ResetLoc(toSay.newResetLocation);
						if(toSay.destroyAfterDialogue){
							GameObject.Destroy(owner.gameObject);
						}
						yield break;
					}
					toSay = owner.GetNextNode();
					if(toSay != null){
						if(toSay.togglePartyMember){
							party.GetUnitStats(toSay.partyMemberToToggle).available = !party.GetUnitStats(toSay.partyMemberToToggle).available;
						}
						if(toSay.mustHaveFlagToContinue != null && toSay.mustHaveFlagToContinue != ""){
							if(!party.flags[toSay.mustHaveFlagToContinue]){
								ui.HideTextHolder();
								owner.ResetLoc(toSay.newResetLocation);
								yield break;
							}
						}
						if(toSay.mustNotHaveFlagToContinue != null && toSay.mustNotHaveFlagToContinue != ""){
							if(party.flags[toSay.mustNotHaveFlagToContinue]){
								ui.HideTextHolder();
								owner.ResetLoc(toSay.newResetLocation);
								yield break;
							}
						}
						if(toSay.showChoice){
							Advance();
							ui.SetChoiceText(toSay.yesText, toSay.noText);
							ui.ShowChoiceHolder();
							allowingInput = false;
						}
						else{
							Advance();
						}
					}
					else{
						ui.HideTextHolder();
						owner.ResetLoc(toSay.newResetLocation);
						yield break;
					}
				}
			}
			yield return null;
		}
	}
	
	public void optionPicked(bool yesOptionPicked){
		ui.HideChoiceHolder();
		if(yesOptionPicked){
			toSay = owner.GetNextNode(toSay.indexOnYes);
			Advance();
			StartCoroutine(WaitAndAllow());
		}
		else{
			toSay = owner.GetNextNode(toSay.indexOnNo);
			Advance();
			StartCoroutine(WaitAndAllow());
		}
	}

	private void Advance(){
		ui.SetDialogueText(toSay.dialogueText);
		if(!toSay.isInformation){
			ui.SetDialogueTitle(toSay.dialogueName);
			ui.SetDialogueHeadSprite(toSay.dialogueHead);
		}
		else{
			ui.SetDialogueTitle("");
			ui.SetDialogueHeadSprite(null);
		}
		if(toSay.animationTarget != null && toSay.animation != null){
			toSay.animationTarget.GetComponent<Animator>().Play(toSay.animation);
		}
		if(toSay.addItem){
			party.AddItemToInventory(toSay.itemToAdd, toSay.itemNumberToAdd);
		}
		if(toSay.removeItem){
			party.RemoveItemFromInventory(toSay.itemToRemove, toSay.itemNumberToRemove);
		}
	}

	IEnumerator WaitAndAllow(){
		yield return new WaitForSeconds(0.1f);
		allowingInput = true;
	}
}
