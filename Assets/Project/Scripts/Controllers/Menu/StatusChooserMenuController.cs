using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusChooserMenuController : MonoBehaviour {

	public GameObject statusChooserPanel;
	public GameObject statusChooserMenuButton;

	private PartyController party;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	public bool SetPause(){
		if(statusChooserPanel.activeSelf){
			HideManageChooserPanel();
			return true;
		}
		return false;
	}

	public void ShowManageChooserPanel(){
		statusChooserPanel.SetActive(true);
	}
	public void HideManageChooserPanel(){
		statusChooserPanel.SetActive(false);
	}
	public void UpdateManageChooserPanel(){
		Button[] buttons = statusChooserPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		int membersAdded = 0;
		for(int i = 0; i< party.playerParty.Length;i++){
			UnitStats p = ((UnitStats)party.playerParty[i].GetComponent("UnitStats"));
			if(p.available){
				GameObject newButton = Instantiate(statusChooserMenuButton);
				StatusChooserMemberButtonController btnCont = newButton.GetComponent<StatusChooserMemberButtonController>();
				btnCont.member = p;
				btnCont.loc = i;
				btnCont.parentPanel = statusChooserPanel;
				membersAdded++;
				if(membersAdded<5){
					newButton.GetComponent<Image>().color = new Color(0.33f,0.7f,0.33f,1.0f);
				}
				else{
				}
			}
		}
	}
}
