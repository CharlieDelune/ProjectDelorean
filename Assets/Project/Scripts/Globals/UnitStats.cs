using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour, IComparable {

	public string charName;
	public int baseAttack;
	public int modAttack;
	public int baseMagAttack;
	public int modMagAttack;
	public int baseDefense;
	public int modDefense;
	public int baseMagDefense;
	public int modMagDefense;
	public int baseAgility;
	public int modAgility;
	public int maxHealth;
	public int currentHealth;
	public int maxMana;
	public int currentMana;
	public int level;
	public int experience;
	public int experienceToNextLevel;
	public string weapon;
	public string armor;
	public string accessory1;
	public string accessory2;

	public Sprite battleHead;

	public string[] battleActions;
	public float battleYValue;
	public GameObject battlePrefab;
	public float worldYValue;

	public float turnCounter;

	public bool dead = false;
	private float hasteValue;

	private int tickSpeed;

	public bool available;
	public List<Ability> charAbilities;
	public string[] abilitiesToGetOnLevelUp;
	public int[] abilityUnlockLevels;
	public Mode charMode;
	public float currentDefaultTurnCounter;
	public GameObject battleAnimator;
	public GameObject battleButton;
	public string defaultAttackAnimation;
	public UnitType unitType;
	public string battleLocation;
	public Facing facing;
	public AbstractEnemyAIController enemyAI;

	void Start(){
		enemyAI = gameObject.GetComponent<AbstractEnemyAIController>();
		charMode = Mode.Attack;
		battleActions = new string[5];
		charAbilities = new List<Ability>();
		AddStartingCharacterAbilities();
		hasteValue = 1;
        CalculateModStats();
		CalculateTickSpeed();
		CalculateTurnCounter(1);
	}

	void Update(){
	}
	public void CalculateTurnCounter(int rank){
		turnCounter = rank * hasteValue * tickSpeed;
		currentDefaultTurnCounter = 3 * hasteValue * tickSpeed;
	}

	public void SetHaste(){
		if(hasteValue == 0.5f){
			hasteValue = 1.0f;
		}
		else if (hasteValue == 1.0f){
			hasteValue = 1.5f;
		}
	}

	public void SetSlow(){
		if(hasteValue == 1.5f){
			hasteValue = 1.0f;
		}
		else if(hasteValue == 1.0f){
			hasteValue = 0.5f;
		}
	}

	public bool IsDead (){
		return dead;
	}

	public int CompareTo (object otherStats){
		return turnCounter.CompareTo (((UnitStats)otherStats).turnCounter);
	}

	public int ReceiveDamage (int damage){
		currentHealth -= damage;
		if(currentHealth <= 0){
			currentHealth = 0;
			dead = true;
		}
		return damage;
	}

	public void RestoreHp(int toRestore){
		if(currentHealth == 0){
			dead = false;
		}
		currentHealth += toRestore;
		if(currentHealth >= maxHealth){
			currentHealth = maxHealth;
		}
	}
	public void RestoreMp(int toRestore){
		currentMana += toRestore;
		if(currentMana >= maxMana){
			currentMana = maxMana;
		}
	}

	public void ReceiveExperience(int newExperience){
		experience += newExperience;
		if(experience >= experienceToNextLevel){
			LevelUp();
		}
	}

	public void LevelUp(){
		level++;
		experienceToNextLevel = (int)Mathf.Round(experienceToNextLevel * 1.5f);
		AddNewCharacterAbilities();
	}

	private void CalculateTickSpeed(){
		if(modAgility == 0){
			tickSpeed = 28;
		}
		else if(modAgility == 1){
			tickSpeed = 26;
		}
		else if(modAgility == 2){
			tickSpeed = 24;
		}
		else if(modAgility == 3){
			tickSpeed = 22;
		}
		else if(modAgility == 4){
			tickSpeed = 20;
		}
		else if(modAgility >= 5 && modAgility <= 6){
			tickSpeed = 16;
		}
		else if(modAgility >= 7 && modAgility <= 9){
			tickSpeed = 15;
		}
		else if(modAgility >= 10 && modAgility <= 11){
			tickSpeed = 14;
		}
		else if(modAgility >= 12 && modAgility <= 14){
			tickSpeed = 13;
		}
		else if(modAgility >= 15 && modAgility <= 16){
			tickSpeed = 12;
		}
		else if(modAgility >= 17 && modAgility <= 18){
			tickSpeed = 11;
		}
		else if(modAgility >= 19 && modAgility <= 22){
			tickSpeed = 10;
		}
		else if(modAgility >= 23 && modAgility <= 28){
			tickSpeed = 9;
		}
		else if(modAgility >= 29 && modAgility <= 34){
			tickSpeed = 8;
		}
		else if(modAgility >= 35 && modAgility <= 43){
			tickSpeed = 7;
		}
		else if(modAgility >= 44 && modAgility <= 61){
			tickSpeed = 6;
		}
		else if(modAgility >= 62 && modAgility <= 97){
			tickSpeed = 5;
		}
		else if(modAgility >= 98 && modAgility <= 169){
			tickSpeed = 4;
		}
		else if(modAgility >= 170 && modAgility <= 255){
			tickSpeed = 3;
		}
	}
	public void CalculateModStats(){
		int atk = baseAttack;
		int magAtk = baseMagAttack;
		int def = baseDefense;
		int magDef = baseMagDefense;
		int agi = baseAgility;
		if(this.weapon != ""){
			atk += ((EquipableItem)Databases.items[Databases.FindItem(weapon)]).increaseAttack;
			magAtk += ((EquipableItem)Databases.items[Databases.FindItem(weapon)]).increaseMagAttack;
			def += ((EquipableItem)Databases.items[Databases.FindItem(weapon)]).increaseDefense;
			magDef += ((EquipableItem)Databases.items[Databases.FindItem(weapon)]).increaseMagDefense;
			agi += ((EquipableItem)Databases.items[Databases.FindItem(weapon)]).increaseAgility;
		}
		if(this.armor != ""){
			atk += ((EquipableItem)Databases.items[Databases.FindItem(armor)]).increaseAttack;
			magAtk += ((EquipableItem)Databases.items[Databases.FindItem(armor)]).increaseMagAttack;
			def += ((EquipableItem)Databases.items[Databases.FindItem(armor)]).increaseDefense;
			magDef += ((EquipableItem)Databases.items[Databases.FindItem(armor)]).increaseMagDefense;
			agi += ((EquipableItem)Databases.items[Databases.FindItem(armor)]).increaseAgility;
		}
		if(this.accessory1 != ""){
			atk += ((EquipableItem)Databases.items[Databases.FindItem(accessory1)]).increaseAttack;
			magAtk += ((EquipableItem)Databases.items[Databases.FindItem(accessory1)]).increaseMagAttack;
			def += ((EquipableItem)Databases.items[Databases.FindItem(accessory1)]).increaseDefense;
			magDef += ((EquipableItem)Databases.items[Databases.FindItem(accessory1)]).increaseMagDefense;
			agi += ((EquipableItem)Databases.items[Databases.FindItem(accessory1)]).increaseAgility;
		}
		if(this.accessory2 != ""){
			atk += ((EquipableItem)Databases.items[Databases.FindItem(accessory2)]).increaseAttack;
			magAtk += ((EquipableItem)Databases.items[Databases.FindItem(accessory2)]).increaseMagAttack;
			def += ((EquipableItem)Databases.items[Databases.FindItem(accessory2)]).increaseDefense;
			magDef += ((EquipableItem)Databases.items[Databases.FindItem(accessory2)]).increaseMagDefense;
			agi += ((EquipableItem)Databases.items[Databases.FindItem(accessory2)]).increaseAgility;
		}
		modAttack = atk;
		modMagAttack = magAtk;
		modDefense = def;
		modMagDefense = magDef;
		modAgility = agi;
	}
	public void AddAbility(Ability a){
		charAbilities.Add(a);
	}
	public GameObject CreateBattleAnimator(GameObject prefab){
		battleAnimator = Instantiate(prefab);
		return battleAnimator;
	}
	public GameObject CreateBattleButton(GameObject prefab){
		battleButton = Instantiate(prefab);
		return battleButton;
	}
	public void AddStartingCharacterAbilities(){
		for(int i = 0; i < abilitiesToGetOnLevelUp.Length;i++){
			if(level >= abilityUnlockLevels[i]){
				charAbilities.Add(Databases.abilities[Databases.FindAbility(abilitiesToGetOnLevelUp[i])]);
			}
		}
	}
	public void AddNewCharacterAbilities(){
		for(int i = 0; i < abilitiesToGetOnLevelUp.Length;i++){
			if(level == abilityUnlockLevels[i]){
				charAbilities.Add(Databases.abilities[Databases.FindAbility(abilitiesToGetOnLevelUp[i])]);
			}
		}
	}
}
public enum UnitType{Friendly,Enemy}
public enum Facing{Left,Right}