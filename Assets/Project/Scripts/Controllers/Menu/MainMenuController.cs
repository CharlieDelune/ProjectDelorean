using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	private PartyController party;
	public GameObject pauseMenuCanvas;
	public GameObject mainPauseMenu;
	public GameObject mainMenuPartyPanel;
	public GameObject mainMenuPartyButton;
	public GameObject dialogueCanvas;
	private bool listeningForStatus;
	private InventoryMenuController inv;
	private StatusMenuController stat;
	private OrderMenuController order;
	private AbilityMenuController ability;
	private SettingsMenuController set;
	[SerializeField]
	private StatusChooserMenuController manage;

	// Use this for initialization
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		inv = GameObject.Find("MenuControllers").GetComponent<InventoryMenuController>();
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		order = GameObject.Find("MenuControllers").GetComponent<OrderMenuController>();
		
		ability = GameObject.Find("MenuControllers").GetComponent<AbilityMenuController>();
		set = GameObject.Find("MenuControllers").GetComponent<SettingsMenuController>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetPause(){
		if(pauseMenuCanvas.activeSelf){
			if(!set.SetPause() && !inv.SetPause() && !ability.SetPause() && !stat.SetPause() && !order.SetPause() && !manage.SetPause()){
				Time.timeScale = 1.0f;
				HideMainPanel();
				pauseMenuCanvas.SetActive(false);	
			}	
		}
		else{
			if(!dialogueCanvas.activeSelf){
				Time.timeScale = 0.0f;
				pauseMenuCanvas.SetActive(true);
				ShowMainPanel();
			}
		}
	}

	public void HideMainPanel(){
		mainPauseMenu.SetActive(false);
	}
	public void ShowMainPanel(){
		mainPauseMenu.SetActive(true);
		UpdateMainMenu();
	}
	public void UpdateMainMenu(){
		foreach(Transform b in mainMenuPartyPanel.transform){
			Destroy (b.gameObject);
		}
		int membersAdded = 0;
		for(int i = 0; i< party.playerParty.Length;i++){
			UnitStats p = ((UnitStats)party.playerParty[i].GetComponent("UnitStats"));
			if(p.available){
				if(membersAdded < 4){
					GameObject newButton = Instantiate(mainMenuPartyButton);
					MainMenuPartyButtonController btnCont = newButton.GetComponent<MainMenuPartyButtonController>();
					btnCont.member = p;
					btnCont.loc = i;
					btnCont.targetPanel = mainMenuPartyPanel;
					membersAdded++;
				}
			}
		}
	}
}