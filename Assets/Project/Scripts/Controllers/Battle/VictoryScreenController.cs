using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenController : MonoBehaviour {
	public GameObject coinsReceivedValueHolder;
	public GameObject coinsCurrentValueHolder;
	public GameObject expReceivedValueHolder;
	public GameObject expRemainderHolder;
	public GameObject expRemainderTextHolder;
	public GameObject itemList;
	public GameObject itemReceivedPrefab;
	public GameObject memberList;
	public GameObject partyMemberPrefab;

	private PartyController playerParty;
	private List<GameObject> partyStatsHolders;
	private List<GameObject> itemHolders;
	private int experienceGained;
	private int remainderExperience;
	private int numPartyMembers;
	private int coinsGained;
	private string[] itemNames;
	private int[] itemCounts;
	private int finalActiveMemberPosition;
	private bool listeningExp;
	private PlayerBehavior player;

	// Use this for initialization
	void Start () {
		partyStatsHolders = new List<GameObject>();
		itemHolders = new List<GameObject>();
		playerParty = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		player = playerParty.player;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.allowingInput){
			if(Input.GetKeyDown(KeyCode.Return)){
				if(listeningExp){
					StopAllCoroutines();
					playerParty.coins += coinsGained;
					coinsGained = 0;
					for(int i=0;i<partyStatsHolders.Count;i++){
						UnitStats member = partyStatsHolders[i].GetComponent<VictoryScreenMemberController>().member;
						if(member.available){
							member.ReceiveExperience(experienceGained);
							partyStatsHolders[i].GetComponent<VictoryScreenMemberController>().RefreshStats();
							RefreshText();
						}
					}
					experienceGained = 0;
					TransferRemainderExperience();
					listeningExp = false;
				}
				else{
					SceneMgmt.WinFight();
				}
			}
		}
	}

	public void GiveRewards(){
		RefreshText();
		CreatePartyMembers();
		StartCoroutine(TransferCurrency());
	}
	public void SetParameters(EnemyPartyController e){
		experienceGained = e.experienceGained;
		remainderExperience = Mathf.RoundToInt(e.experienceGained / 2);
		coinsGained = e.coinsGained;
		itemNames = e.itemNames;
		itemCounts = e.itemCounts;
	}
	public void RefreshText(){
		coinsReceivedValueHolder.GetComponent<Text>().text = coinsGained + "c";
		coinsCurrentValueHolder.GetComponent<Text>().text = playerParty.coins + "c";
		expReceivedValueHolder.GetComponent<Text>().text = experienceGained.ToString();
		expRemainderTextHolder.GetComponent<Text>().text = "Remaining party members received " + remainderExperience + " experience.";
	}
	public void CreateItems(){
		for(int i=0;i<itemNames.Length;i++){
			GameObject g = Instantiate(itemReceivedPrefab);
			itemHolders.Add(g);
			g.GetComponent<VictoryScreenItemController>().SetParameters(itemList,itemNames[i],itemCounts[i]);
			playerParty.AddItemToInventory(itemNames[i],itemCounts[i]);
		}
		StartCoroutine(TransferExperience());
	}
	public void CreatePartyMembers(){
		for(int i=0;i<playerParty.playerParty.Length;i++){
			if(numPartyMembers < 4){
				UnitStats member = playerParty.playerParty[i].GetComponent<UnitStats>();
				if(member.available){
					GameObject g = Instantiate(partyMemberPrefab);
					partyStatsHolders.Add(g);
					g.GetComponent<VictoryScreenMemberController>().SetParameters(memberList,member);
					numPartyMembers++;
					finalActiveMemberPosition = i+1;
				}
			}
		}
	}

	public IEnumerator TransferCurrency(){
		listeningExp = true;
		yield return new WaitForSeconds(1.0f);
		while(coinsGained > 0){
			coinsGained--;
			playerParty.coins++;
			RefreshText();
			yield return 0;
		}
		CreateItems();
	}
	public IEnumerator TransferExperience(){
		while(experienceGained > 0){
			experienceGained--;
			for(int i=0;i<partyStatsHolders.Count;i++){
				UnitStats member = partyStatsHolders[i].GetComponent<VictoryScreenMemberController>().member;
				if(member.available){
					member.ReceiveExperience(1);
					partyStatsHolders[i].GetComponent<VictoryScreenMemberController>().RefreshStats();
					RefreshText();
				}
			}
			yield return 0;
		}
		listeningExp = false;
		TransferRemainderExperience();
	}
	public void TransferRemainderExperience(){
		for(int i = finalActiveMemberPosition;i < playerParty.playerParty.Length;i++){
			UnitStats member = playerParty.playerParty[i].GetComponent<UnitStats>();
			if(member.available){
				expRemainderHolder.SetActive(true);
				member.ReceiveExperience(remainderExperience);
			}
		}
	}
}
