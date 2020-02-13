using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

	private PartyController party;
	private AbilityMenuController ac;
	private StatusMenuController stat;
	private int toUse;
	private UnitStats target;
	private UnitStats user;

	// Use this for initialization
	void Start () {
		toUse = -1;
		ac = GameObject.Find("MenuControllers").GetComponent<AbilityMenuController>();
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(toUse != -1 && user != null && target != null){
			UseAbilityOnTarget();
		}
	}

	private void UseAbilityOnTarget(){
		if(user.charAbilities[toUse].useableOutOfCombat && (user.currentMana >= user.charAbilities[toUse].mpCost)){
			Ability ability = user.charAbilities[toUse];
			bool used = false;
			if(ability.targetType==TargetType.One){
				if(ability.heal > 0 && target.currentHealth < target.maxHealth){
					target.RestoreHp(Mathf.RoundToInt(user.modMagAttack * ability.heal));
					used = true;
				}
			}
			else if(ability.targetType==TargetType.AllFriendly){
				foreach(GameObject member in party.playerParty){
					UnitStats memberStats = member.GetComponent<UnitStats>();
					if(ability.heal > 0 && memberStats.currentHealth < memberStats.maxHealth){
						memberStats.RestoreHp(Mathf.RoundToInt(user.modMagAttack * ability.heal));
						used = true;
					}
				}
			}
			if(used){
				user.currentMana -= ability.mpCost;
				ac.UpdateTargetPanel();
				stat.UpdateStatusText();
				if(user.currentMana < ability.mpCost){
					ac.HideTargetPanel();
					toUse = -1;
				}
				target = null;
			}
		}
		else{
			//SHOW SOME KIND OF WARNING
		}
	}
	public void SetUser(UnitStats us){
		user = us;
	}
	public int GetToUse(){
		return toUse;
	}
	public void SetToUse(int i){
		toUse = i;
	}
	public UnitStats GetTarget(){
		return target;
	}
	
	public void SetTarget(UnitStats us){
		target = us;
	}
}
