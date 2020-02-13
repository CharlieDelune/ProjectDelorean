using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItemController : MonoBehaviour {
	public Action action;
	public BattleFlowController flow;
	public BattleUIController ui;
	private PartyController party;
	public Item item;
	public int itemNum;
	void Start () {
		party = GameObject.Find("PlayerParty").GetComponent<PartyController>();
		flow = GameObject.Find("BattleControllers").GetComponent<BattleFlowController>();
		ui = GameObject.Find("BattleControllers").GetComponent<BattleUIController>();
		action = Action.Item;
	}
	
	// Update is called once per frame
	public void UseItem(UnitStats target){
		StartCoroutine(WaitAndThenGetHit(target));
		flow.SetUpNextTurn(2);
	}
	public void UseItem(UnitStats[] target){
		StartCoroutine(WaitAndThenGetHit(target));
		flow.SetUpNextTurn(2);
	}
	IEnumerator WaitAndThenGetHit(UnitStats target){
		ui.DisableMainButtons();
		ui.DisableTargetButtons();
		bool used = false;
		if(item.itemType == ItemType.Consumable){
			ConsumableItem consumable = (ConsumableItem)Databases.items[itemNum];
			if(consumable.targetType == TargetType.One){
				if(consumable.hpRestore > 0 && target.currentHealth < target.maxHealth){
					target.RestoreHp(consumable.hpRestore);
					ui.ShowHealText(target,consumable.hpRestore);
					used = true;
				}
				if(consumable.mpRestore > 0 && target.currentMana < target.maxMana){
					target.RestoreMp(consumable.mpRestore);
					used = true;
				}
			}
			else if(consumable.targetType == TargetType.AllFriendly){
				foreach(UnitStats member in flow.friendlyUnits){
					UnitStats memberStats = member.GetComponent<UnitStats>();
					if(memberStats.available == true){
						if(consumable.hpRestore > 0 && memberStats.currentHealth < memberStats.maxHealth){
							memberStats.RestoreHp(consumable.hpRestore);
							ui.ShowHealText(memberStats,consumable.hpRestore);
							used = true;
						}
						if(consumable.mpRestore > 0 && memberStats.currentMana < memberStats.maxMana){
							memberStats.RestoreMp(consumable.mpRestore);
							used = true;
						}
					}
				}
			}
			else if(consumable.targetType == TargetType.All){
				if(consumable.hpRestore > 0 && flow.currentUnit.currentHealth < flow.currentUnit.maxHealth){
					flow.currentUnit.RestoreHp(consumable.hpRestore);
					ui.ShowHealText(flow.currentUnit,consumable.hpRestore);
					used = true;
				}
				foreach(UnitStats member in flow.units){
					UnitStats memberStats = member.GetComponent<UnitStats>();
					if(memberStats.available == true){
						if(consumable.hpRestore > 0 && memberStats.currentHealth < memberStats.maxHealth){
							memberStats.RestoreHp(consumable.hpRestore);
							ui.ShowHealText(memberStats,consumable.hpRestore);
							used = true;
						}
						if(consumable.mpRestore > 0 && memberStats.currentMana < memberStats.maxMana){
							memberStats.RestoreMp(consumable.mpRestore);
							used = true;
						}
					}
				}
			}
		}
		if(used){
			party.RemoveItemFromInventory(itemNum);
			flow.currentUnit.battleAnimator.GetComponent<Animator>().Play("Thrust" + target.facing.ToString());
			yield return new WaitForSeconds(0.33f);
			if(target.IsDead()){
				target.battleAnimator.GetComponent<Animator>().Play("Die");
				flow.units.Remove(target);
			}
			else{
				target.battleAnimator.GetComponent<Animator>().Play("Hit" + target.facing.ToString());
			}
			yield return new WaitForSeconds(1.0f);
		}
		else{
			ui.EnableMainButtons();
		}
	}
	IEnumerator WaitAndThenGetHit(UnitStats[] tList){
		foreach(UnitStats target in tList){
			ui.DisableMainButtons();
			ui.DisableTargetButtons();
			bool used = false;
			if(item.itemType == ItemType.Consumable){
				ConsumableItem consumable = (ConsumableItem)item;
				if(consumable.targetType == TargetType.One){
					if(consumable.hpRestore > 0 && target.currentHealth < target.maxHealth){
						target.RestoreHp(consumable.hpRestore);
						ui.ShowHealText(target,consumable.hpRestore);
						used = true;
					}
					if(consumable.mpRestore > 0 && target.currentMana < target.maxMana){
						target.RestoreMp(consumable.mpRestore);
						used = true;
					}
				}
				else if(consumable.targetType == TargetType.AllFriendly){
					foreach(GameObject member in party.playerParty){
						UnitStats memberStats = member.GetComponent<UnitStats>();
						if(memberStats.available == true){
							if(consumable.hpRestore > 0 && memberStats.currentHealth < memberStats.maxHealth){
								memberStats.RestoreHp(consumable.hpRestore);
								ui.ShowHealText(memberStats,consumable.hpRestore);
								used = true;
							}
							if(consumable.mpRestore > 0 && memberStats.currentMana < memberStats.maxMana){
								memberStats.RestoreMp(consumable.mpRestore);
								used = true;
							}
						}
					}
				}
				if(consumable.damageMod > 0){
					target.ReceiveDamage(Mathf.RoundToInt(consumable.damageMod));
				}
			}
			if(used){
				party.RemoveItemFromInventory(itemNum);
				flow.currentUnit.battleAnimator.GetComponent<Animator>().Play("Thrust");
				yield return new WaitForSeconds(0.33f);
				if(target.IsDead()){
					target.battleAnimator.GetComponent<Animator>().Play("Die");
					flow.units.Remove(target);
				}
				else{
					target.battleAnimator.GetComponent<Animator>().Play("Hit");
				}
				yield return new WaitForSeconds(1.0f);
			}
			else{
				ui.EnableMainButtons();
			}
		}
	}
	public void SetItem(int i){
		itemNum = i;
		item = Databases.items[i];
	}
}
