using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMenuController : MonoBehaviour {

	public GameObject orderPanel;
	public GameObject orderMemberButton;
	private int firstMember,secondMember;
	private PartyController party;
	private MainMenuController main;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		main = GameObject.Find("MenuControllers").GetComponent<MainMenuController>();
		firstMember = -1; secondMember = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if((firstMember > -1 && firstMember < party.playerParty.Length) && (secondMember > -1 && secondMember < party.playerParty.Length)){
			party.SwapPartyMembers(firstMember,secondMember);
			UpdateOrderPanel();
			main.UpdateMainMenu();
			firstMember = -1; secondMember = -1;
		}
	}
	public bool SetPause(){
		if(orderPanel.activeSelf){
			HideOrderPanel();
			return true;
		}
		return false;
	}

	public void ShowOrderPanel(){
		orderPanel.SetActive(true);
	}
	public void HideOrderPanel(){
		orderPanel.SetActive(false);
	}
	public void UpdateOrderPanel(){
		Button[] buttons = orderPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		int membersAdded = 0;
		for(int i = 0; i< party.playerParty.Length;i++){
			UnitStats p = ((UnitStats)party.playerParty[i].GetComponent("UnitStats"));
			if(p.available){
				GameObject newButton = Instantiate(orderMemberButton);
				OrderMemberButtonController btnCont = newButton.GetComponent<OrderMemberButtonController>();
				btnCont.member = p;
				btnCont.loc = i;
				btnCont.parentPanel = orderPanel;
				membersAdded++;
				if(membersAdded<5){
					newButton.GetComponent<Image>().color = new Color(0.33f,0.7f,0.33f,1.0f);
				}
				else{
				}
			}
		}
	}
	public void SetMember(int i){
		if(firstMember == -1){
			firstMember = i;
		}
		else if (secondMember == -1){
			secondMember = i;
		}
		else{
			Debug.Log("Forgot to reset members somewhere.");
		}
	}
}
