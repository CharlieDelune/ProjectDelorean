using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleAbilityController : MonoBehaviour {
	public Action action;
	public BattleFlowController flow;
	public BattleUIController ui;
	public Ability ability;
	void Start () {
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		action = Action.Ability;
	}
	// Update is called once per frame
	public void UseAbility(UnitStats target){
		StartCoroutine(WaitAndThenGetHit(target));
	}
	public void UseAbility(UnitStats[] target){
		StartCoroutine(WaitAndThenGetHit(target));
	}
	IEnumerator WaitAndThenGetHit(UnitStats target){
		ui.DisableMainButtons();
		ui.DisableTargetButtons();
		bool used = false;
		if(flow.currentUnit.currentMana >= ability.mpCost){
			flow.currentUnit.battleAnimator.GetComponent<Animator>().Play(ability.defaultAttackAnimation + flow.currentUnit.facing.ToString());
			yield return new WaitForSeconds(0.33f);
			if(ability.targetType == TargetType.One){
				if(ability.heal > 0 && target.currentHealth < target.maxHealth){
					int toHeal = Mathf.RoundToInt(flow.currentUnit.modMagAttack * ability.heal);
					target.RestoreHp(toHeal);
					ui.ShowHealText(target,toHeal);
					used = true;
				}
				else if(ability.damage > 0){
					DealDamageToTarget(target);
					used = true;
				}
			}
			else if(ability.targetType == TargetType.AllFriendly){
				foreach(UnitStats member in flow.friendlyUnits){
				UnitStats memberStats = member.GetComponent<UnitStats>();
					if(ability.heal > 0 && memberStats.currentHealth < memberStats.maxHealth){
						int toHeal = Mathf.RoundToInt(flow.currentUnit.modMagAttack * ability.heal);
						memberStats.RestoreHp(toHeal);
						ui.ShowHealText(memberStats,toHeal);
						used = true;
					}
					else if(ability.damage > 0){
						DealDamageToTarget(memberStats);
						used = true;
					}
				}
			}
			else if(ability.targetType == TargetType.All){
				if(ability.heal > 0 && flow.currentUnit.currentHealth < flow.currentUnit.maxHealth){
					int toHeal = Mathf.RoundToInt(flow.currentUnit.modMagAttack * ability.heal);
					flow.currentUnit.RestoreHp(toHeal);
					ui.ShowHealText(flow.currentUnit,toHeal);
					used = true;
				}
				else if(ability.damage > 0){
					DealDamageToTarget(flow.currentUnit);
					used = true;
				}
				foreach(UnitStats member in flow.units){
					UnitStats memberStats = member.GetComponent<UnitStats>();
					if(ability.heal > 0 && memberStats.currentHealth < memberStats.maxHealth){
						int toHeal = Mathf.RoundToInt(flow.currentUnit.modMagAttack * ability.heal);
						memberStats.RestoreHp(toHeal);
						ui.ShowHealText(memberStats,toHeal);
						used = true;
					}
					else if(ability.damage > 0){
						DealDamageToTarget(memberStats);
						used = true;
					}
				}
			}
		}
		if(used){
			flow.currentUnit.currentMana -= ability.mpCost;
			flow.SetUpNextTurn(ability.speedRank);
		}
		else{
			ui.EnableMainButtons();
		}
	}
	IEnumerator WaitAndThenGetHit(UnitStats[] tList){
		ui.DisableMainButtons();
		ui.DisableTargetButtons();
		bool used = false;
		if(flow.currentUnit.currentMana >= ability.mpCost){
			flow.currentUnit.currentMana -= ability.mpCost;
			flow.currentUnit.battleAnimator.GetComponent<Animator>().Play(ability.defaultAttackAnimation + flow.currentUnit.facing.ToString());
			yield return new WaitForSeconds(0.33f);
			foreach(UnitStats target in tList){
				if(ability.heal > 0 && target.currentHealth < target.maxHealth){
					int toHeal = Mathf.RoundToInt(flow.currentUnit.modMagAttack * ability.heal);
					target.RestoreHp(toHeal);
					ui.ShowHealText(target,toHeal);
					used = true;
				}
				else if(ability.damage > 0){
					DealDamageToTarget(target);
					used = true;
				}
			}
		}
		if(used){
			flow.SetUpNextTurn(ability.speedRank);
		}
		else{
			ui.EnableMainButtons();
		}
	}
	public void SetAbility(int i){
		ability = flow.currentUnit.charAbilities[i];
	}

	public void DealDamageToTarget(UnitStats target){
		if(ability.abilityType == AbilityType.Physical){
			int dmg = Mathf.RoundToInt(ability.damage * (((flow.currentUnit.modAttack * flow.currentUnit.modAttack)/6)+ability.damage)/4);
			int defNum = Mathf.RoundToInt((((target.modDefense - 280.4f) * (target.modDefense - 280.4f)/110)+16));
			int dmg2 = (dmg * defNum) / 730;
			int finalDmg = dmg2 * (730 - (target.modDefense * 51 - (target.modDefense * target.modDefense) / 11)/10)/730;
			int modFinalDmg = finalDmg;
			if(target.charMode == Mode.Defense){
				modFinalDmg = Mathf.RoundToInt(modFinalDmg/2);
			}
			target.ReceiveDamage(modFinalDmg);
			ui.ShowDamageText(target,modFinalDmg);
		}
		else{
			int dmg = Mathf.RoundToInt(ability.damage * (((flow.currentUnit.modMagAttack * flow.currentUnit.modMagAttack)/6)+ability.damage)/4);
			int defNum = Mathf.RoundToInt((((target.modMagDefense - 280.4f) * (target.modMagDefense - 280.4f)/110)+16));
			int dmg2 = (dmg * defNum) / 730;
			int finalDmg = dmg2 * (730 - (target.modMagDefense * 51 - (target.modMagDefense * target.modMagDefense) / 11)/10)/730;
			int modFinalDmg = finalDmg;
			if(target.charMode == Mode.Defense){
				modFinalDmg = Mathf.RoundToInt(modFinalDmg/2);
			}
			target.ReceiveDamage(Mathf.RoundToInt(modFinalDmg));
			ui.ShowDamageText(target,modFinalDmg);
		}
		if(target.IsDead()){
			target.battleAnimator.GetComponent<Animator>().Play("Die");
			flow.units.Remove(target);
		}
		else{
			target.battleAnimator.GetComponent<Animator>().Play("Hit" + target.facing.ToString());
		}
	}
}
