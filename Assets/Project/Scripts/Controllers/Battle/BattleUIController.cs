using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUIController : MonoBehaviour {
	public GameObject partyStatPanel;
	public GameObject partyStatButton;
	public GameObject enemyStatPanel;
	public GameObject enemyStatButton;
	public GameObject actionPanel;
	public GameObject modeConfirmationPanel;
	public Text modeConfirmationText;
	public GameObject movementHolder;
	public GameObject movementPanel;
	public GameObject inventoryPanel;
	public GameObject itemPanel;
	public GameObject itemPrefab;
	public GameObject abilityHolder;
	public GameObject abilityPanel;
	public GameObject abilityPrefab;
	public GameObject damageText;
	public GameObject healText;
	public GameObject victoryHolder;
	public GameObject victoryMenu;
	private BattleFlowController flow;
	private PartyController party;
	private EnemyPartyController enemies;
	private List<BattlePartyButtonController> pbtns;
	private List<UnitTurn> turnList;
	private PlayerBehavior player;


	// Use this for initialization
	void Start () {
		pbtns = new List<BattlePartyButtonController>();
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		player = GameObject.Find("PlayerParty").GetComponent<PartyController>().player;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Escape)){
			SetPause();
		}
	}

	public void SetPause(){
		if(modeConfirmationPanel.activeSelf){
			HideModeConfirmation();
		}
		if(itemPanel.activeSelf){
			HideInventoryPanel();
		}
		if(abilityHolder.activeSelf){
			HideAbilityPanel();
		}
		if(movementHolder.activeSelf){
			HideMoveHolder();
		}
		DisableTargetButtons();
	}

	public void SetPartyAndEnemies(PartyController p, EnemyPartyController e){
		party = p;
		enemies = e;
	}

	public void CreatePartyButton(int i, int memberNum){
		GameObject friendlySpace = GameObject.Find("FriendlyRight" + memberNum);
		UnitStats member = party.playerParty[i].GetComponent<UnitStats>();
		member.battleLocation = ("FriendlyRight" + memberNum);
		member.facing = Facing.Left;
		GameObject thisMember = member.CreateBattleAnimator(member.battlePrefab);
		thisMember.transform.parent = friendlySpace.transform;
		thisMember.transform.localPosition = new Vector3(0,thisMember.transform.position.y,0);
		if(!member.IsDead()){
			thisMember.GetComponent<Animator>().Play("BattleIdle" + member.facing.ToString());
		}
		else{
			thisMember.GetComponent<Animator>().Play("Dead");
		}
		GameObject mbtn = member.CreateBattleButton(partyStatButton);
		BattlePartyButtonController btn = mbtn.GetComponent<BattlePartyButtonController>();
		btn.AddParent(partyStatPanel);
		btn.member = party.playerParty[i].GetComponent<UnitStats>();
		btn.UpdateText();
		pbtns.Add(btn);
	}
	public void CreateEnemyButton(int i){
		GameObject unfriendlySpace = GameObject.Find("EnemyMid" + (i+1));
		UnitStats member = enemies.enemyParty[i].GetComponent<UnitStats>();
		member.battleLocation = "EnemyMid" + (i+1);
		member.facing = Facing.Right;
		GameObject thisMember = member.CreateBattleAnimator(member.battlePrefab);
		thisMember.transform.parent = unfriendlySpace.transform;
		thisMember.transform.localPosition = new Vector3(0,thisMember.transform.position.y,0);
		thisMember.GetComponent<Animator>().Play("BattleIdle" + member.facing.ToString());
		GameObject ebtn = member.CreateBattleButton(enemyStatButton);
		BattleEnemyButtonController btn = ebtn.GetComponent<BattleEnemyButtonController>();
		btn.AddParent(enemyStatPanel);
		btn.member = enemies.enemyParty[i].GetComponent<UnitStats>();
		btn.UpdateText();
	}
	public void UpdateTurnOrder(){
		TurnArray t = new TurnArray();
		turnList = t.turnList;
		for(int i = 0;i < flow.units.Count;i++){
			turnList.Add(new UnitTurn(flow.units[i],flow.units[i].turnCounter));
		}
		for(int sentinel = 1; sentinel < 6; sentinel++){
			for (int i = 0; i < flow.units.Count; i++){
				turnList.Add(new UnitTurn(flow.units[i], (sentinel * flow.units[i].currentDefaultTurnCounter) + flow.units[i].turnCounter));
			}
		}
		turnList.Sort();
		for(int i=0;i<10;i++){
			GameObject.Find("Turn" + i + "Image").GetComponent<Image>().sprite = turnList[i].unit.battleHead;
		}
	}
	public void UpdateActionButtons(UnitStats currentUnit){
		Button[] buttons = actionPanel.GetComponentsInChildren<Button>();
        foreach (Button b in buttons)
        {
			if(b.GetComponentInChildren<Text>().text == "Attack" && flow.currentUnit.charMode == Mode.Defense){
				b.GetComponentInChildren<Text>().text = "Defend";
			}
			else if (b.GetComponentInChildren<Text>().text == "Defend" && flow.currentUnit.charMode == Mode.Attack){
				b.GetComponentInChildren<Text>().text = "Attack";
			}
			else {
			}
            if(b.GetComponentInChildren<Text>().text == "Ability" && flow.currentUnit.charAbilities.Count == 0){
				b.interactable = false;
			}
			else{
				b.interactable = true;
			}
        }
	}
	public void ShowModeConfirmation(){
		string name = flow.currentUnit.charName;
		Mode currMode = flow.currentUnit.charMode;
		string changeMode = "";
		if(currMode == Mode.Attack){
			changeMode = "Defense";
		}
		else if (currMode == Mode.Defense){
			changeMode = "Attack";
		}
		modeConfirmationText.text = "Change " + name + " to " + changeMode + " mode?";
		modeConfirmationPanel.SetActive(true);
	}
	public void HideModeConfirmation(){
		modeConfirmationPanel.SetActive(false);
	}
	public void UpdatePartyButtonText(){
		foreach(BattlePartyButtonController b in pbtns){
			b.UpdateText();
		}
	}
	public void ShowDamageText(UnitStats target, int text){
		GameObject dmgText;
		if(text >= 0){
			dmgText = Instantiate(damageText);
		}
		else{
			dmgText = Instantiate(healText);
		}
		dmgText.transform.SetParent(GameObject.Find(target.battleLocation + "Text").transform,false);
		dmgText.GetComponent<BattleDamageHealTextController>().UpdateText(text.ToString());
		dmgText.GetComponent<BattleDamageHealTextController>().StartAnimation();
	}
	public void ShowHealText(UnitStats target, int text){
		GameObject hlText;
		if(text >= 0){
			hlText = Instantiate(healText);
		}
		else{
			hlText = Instantiate(healText);
		}
		hlText.transform.SetParent(GameObject.Find(target.battleLocation + "Text").transform,false);
		hlText.GetComponent<BattleDamageHealTextController>().UpdateText(text.ToString());
		hlText.GetComponent<BattleDamageHealTextController>().StartAnimation();
	}
	public void DisableMainButtons(){
		Button[] buttons = actionPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = false;
		}
	}
	public void EnableMainButtons(){
		Button[] buttons = actionPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = true;
		}
	}
	public void DisableTargetButtons(){
		Button[] buttons = partyStatPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = false;
		}
		buttons = enemyStatPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = false;
		}
	}
	public void EnableTargetButtons(){
		Button[] buttons = partyStatPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			b.interactable = true;
		}
		buttons = enemyStatPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			if(!b.gameObject.GetComponent<BattleEnemyButtonController>().member.IsDead()){
				b.interactable = true;
			}
		}
	}
	public void ShowInventoryPanel(){
		DisableMainButtons();
		inventoryPanel.SetActive(true);
		Button[] buttons = itemPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		if(party.InventoryNotEmpty()){
			for(int i = 0; i < party.playerInventoryCount.Length; i++){
				if(party.playerInventoryCount[i] > 0){
					GameObject btn = Instantiate (itemPrefab);
					Button theBtn = btn.GetComponent<Button>();
					BattleInventoryButtonController btnCont = btn.GetComponent<BattleInventoryButtonController>();
					btnCont.invPanel = itemPanel;
					btnCont.invItemNum = i;
					if(Databases.items[i].itemType == ItemType.Consumable && ((ConsumableItem)Databases.items[i]).useableOutOfCombat == true){
						theBtn.interactable = true;
					}
				}
			}
		}
	}
	public void HideInventoryPanel(){
		EnableMainButtons();
		inventoryPanel.SetActive(false);
	}
	public void ShowAbilityPanel(){
		DisableMainButtons();
		abilityHolder.SetActive(true);
		Button[] buttons = abilityPanel.GetComponentsInChildren<Button>();
		foreach(Button b in buttons){
			Destroy (b.gameObject);
		}
		if(flow.currentUnit.charAbilities.Count >= 1){
			for(int i = 0; i < flow.currentUnit.charAbilities.Count; i++){
				GameObject btn = Instantiate (abilityPrefab);
				Button theBtn = btn.GetComponent<Button>();
				BattleAbilityButtonController btnCont = btn.GetComponent<BattleAbilityButtonController>();
				btnCont.holderPanel = abilityPanel;
				btnCont.abilityNum = i;
				if(flow.currentUnit.currentMana >= flow.currentUnit.charAbilities[i].mpCost){
					theBtn.interactable = true;
				}
			}
		}
	}
	public void HideAbilityPanel(){
		EnableMainButtons();
		abilityHolder.SetActive(false);
	}

	public void ShowMoveHolder(){
		DisableMainButtons();
		movementHolder.SetActive(true);
		foreach(Transform child in movementPanel.transform){
			if(child.gameObject.name.Contains("Friendly")){
				child.gameObject.GetComponent<Button>().interactable = true;
			}
			foreach(Transform grandchild in child.transform){
				if(grandchild.gameObject.name.Contains("Face")){
					grandchild.gameObject.GetComponent<Image>().sprite = null;
				}
			}
			
		}
		GameObject currentFace = GameObject.Find(flow.currentUnit.battleLocation + "ImageFace");
		GameObject currentHolder = currentFace.transform.parent.gameObject;
		currentFace.GetComponent<Image>().sprite = flow.currentUnit.battleHead;
		currentHolder.GetComponent<Button>().interactable = false;
		foreach(UnitStats member in flow.units){
			GameObject memberFace = GameObject.Find(member.battleLocation + "ImageFace");
			GameObject memberHolder = currentFace.transform.parent.gameObject;
			memberFace.GetComponent<Image>().sprite = member.battleHead;
			memberHolder.GetComponent<Button>().interactable = false;
		}
	}
	public void HideMoveHolder(){
		EnableMainButtons();
		movementHolder.SetActive(false);
	}
	public void ShowVictoryScreen(){
		StartCoroutine(FadeInVictoryScreenCoroutine());
	}
	public IEnumerator FadeInVictoryScreenCoroutine(){
		player.allowingInput = false;
		victoryHolder.SetActive(true);
		float time = 0;
		while(time < 1.0f){
			time += Time.deltaTime;
			Color temp = victoryHolder.GetComponent<Image>().color;
			temp.a += Time.deltaTime;
			victoryHolder.GetComponent<Image>().color = temp;
			yield return null;
		}
		victoryMenu.SetActive(true);
		player.allowingInput = true;
		gameObject.GetComponent<VictoryScreenController>().SetParameters(flow.enemies);
		gameObject.GetComponent<VictoryScreenController>().GiveRewards();
	}
}
public class TurnArray {
	public List<UnitTurn> turnList;

	public TurnArray(){
		turnList = new List<UnitTurn>();
	}
	public void InsertAt(UnitTurn u,int i){
		for(int j = turnList.Count; j >= i; j--){
			if(j == i){
				turnList[i] = u;
			}
			else{		
				if(j>0){
					if(j != turnList.Count){
						
                    	turnList[j] = turnList[j - 1];
					}
					else{
						turnList.Add(u);
					}
				}
			}
		}
	}
}
public class UnitTurn : IComparable{
	public UnitStats unit;
	public float totalTurnCounter;

	public UnitTurn(UnitStats u, float ttc){
		unit = u;
		totalTurnCounter = ttc;
	}
	public int CompareTo (object otherTurn){
		if(totalTurnCounter.CompareTo(((UnitTurn)otherTurn).totalTurnCounter) == 0){
			if(unit.modAgility.CompareTo(((UnitTurn)otherTurn).unit.modAgility) == 0){
				if(unit.baseAgility.CompareTo(((UnitTurn)otherTurn).unit.baseAgility) == 0){
					if(unit.unitType == UnitType.Friendly && ((UnitTurn)otherTurn).unit.unitType == UnitType.Enemy){
						return -1;
					}
					else{
						return 1;
					}
				}
				return ((UnitTurn)otherTurn).unit.baseAgility.CompareTo(unit.baseAgility);
			}
			return ((UnitTurn)otherTurn).unit.modAgility.CompareTo(unit.modAgility);
		}
		return totalTurnCounter.CompareTo (((UnitTurn)otherTurn).totalTurnCounter);

	}
}