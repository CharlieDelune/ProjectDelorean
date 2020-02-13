using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Databases {

	//Base: Name,Description,Buy value, sell value, itemtype
	//Consumable: hpRestore,mpRestore,haste,attackUp,defesseUp,damageMod,useableOutofCombat
	//Equipapable: Location, attack, magatk,def,magdef, agi, equippableby
	public static Item[] items = new Item[]{
		new ConsumableItem("Weak Potion","Restores 200 health to one party member.",10,5,ItemType.Consumable,TargetType.One,200,0,false,0,0,0,0,true),
		new ConsumableItem("Normal Potion","Restores 500 health to one party member.",10,5,ItemType.Consumable,TargetType.One,500,0,false,0,0,0,0,true),
		new ConsumableItem("Strong Potion","Restores 1000 health to one party member.",10,5,ItemType.Consumable,TargetType.One,1000,0,false,0,0,0,0,true),
		new ConsumableItem("Stronger Potion","Restores 3000 health to one party member.",10,5,ItemType.Consumable,TargetType.One,3000,0,false,0,0,0,0,true),
		new ConsumableItem("Ludicrous Potion","Restores all health to one party member.",10,5,ItemType.Consumable,TargetType.One,999999999,0,false,0,0,0,0,true),
		new ConsumableItem("Strong All-Potion","Restores 1500 health to all party members.",10,5,ItemType.Consumable,TargetType.AllFriendly,1500,0,false,0,0,0,0,true),
		new ConsumableItem("Weak Ointment","Restores 50 mana to one party member.",10,5,ItemType.Consumable,TargetType.One,0,50,false,0,0,0,0,true),
		new ConsumableItem("Normal Ointment","Restores 150 mana to one party member.",10,5,ItemType.Consumable,TargetType.One,0,150,false,0,0,0,0,true),
		new ConsumableItem("Strong Ointment","Restores 300 mana to one party member.",10,5,ItemType.Consumable,TargetType.One,0,300,false,0,0,0,0,true),
		new ConsumableItem("Stronger Ointment","Restores 500 mana to one party member.",10,5,ItemType.Consumable,TargetType.One,0,500,false,0,0,0,0,true),
		new EquipableItem("Wooden Sword","A dull sword made of wood.",100,50,ItemType.Equipable,TargetType.None,"weapon",1,0,0,0,0,new string[]{"Mason","Valdis"}),
		new EquipableItem("Wooden Wand","A weak wand made of wood.",100,50,ItemType.Equipable,TargetType.None,"weapon",0,1,0,0,0,new string[]{"Eira"}),
		new EquipableItem("Wooden Mace","A flimsy mace made of wood.",100,50,ItemType.Equipable,TargetType.None,"weapon",0,1,0,0,0,new string[]{"Garf"}),
		new EquipableItem("Shoddy Bow","A shoddy bow made of wood.",100,50,ItemType.Equipable,TargetType.None,"weapon",1,0,0,0,0,new string[]{"Koda"}),
		new EquipableItem("Rusted Spear","A cheap, old spear.",100,50,ItemType.Equipable,TargetType.None,"weapon",1,1,0,0,0,new string[]{"Bhirt"}),
		new EquipableItem("Cloth Armor","Regular clothing, provides little defense.",100,50,ItemType.Equipable,TargetType.None,"armor",0,0,1,1,0,new string[]{"Mason","Garf","Eira","Valdis","Koda"}),
		new EquipableItem("Copper Ring","Wearing a loop of metal somehow makes you faster.",100,50,ItemType.Equipable,TargetType.None,"acc1",0,0,0,0,1,new string[]{"Garf"})
		};
	
	//Name, useableOutOfCombat, mpCost, speed rank, damage, healing, apply haste, delay, atkdown,magatkdown,defdown,magdefdown,agidown,defaultanim
	public static Ability[] abilities = new Ability[]{
		new Ability("DamageSpell1", "Small dmg to one foe",AbilityType.Magical, false, 5, 3, 100,0,false,0,0,0,0,0,0,"Attack",TargetType.One),
		new Ability("Cure","small recovery to one target",AbilityType.Magical,true,15,3,0,200,false,0,0,0,0,0,0,"Cast",TargetType.One),
		new Ability("Cura","small recovery to all target",AbilityType.Magical,true,35,3,0,150,false,0,0,0,0,0,0,"Cast",TargetType.AllFriendly),
		new Ability("Hard Thrust","Large dmg to all foes",AbilityType.Physical, false, 0, 5,9,0,false,0,0,0,0,0,0,"Thrust",TargetType.One)
	};

	public static int FindItem(string toFind){
		int item = -1;
		for(int i = 0;i < items.Length; i++){
			if(toFind == items[i].itemName){
				item = i;
			}
		}
		return item;
	}
	public static int FindAbility(string toFind){
		int ability = -1;
		for(int i = 0;i < abilities.Length; i++){
			if(toFind == abilities[i].abilityName){
				ability = i;
			}
		}
		return ability;
	}
	public static float RoundToNearest(float toRound, float roundingRule){
		return roundingRule * Mathf.Round(toRound/roundingRule);
	}
}
public enum Direction
{
    Up,
    Right,
    Down,
    Left
}
public enum Action{
	Null,
	Attack,
	Defend,
	Ability,
	Move,
	Mode,
	Item
}
public enum Mode{
	Attack,
	Defense
}
public enum ItemType {Consumable,Equipable,Story};
public enum TargetType{None,One,All,AllInFront,AllBehind,AllFriendly,AllEnemy}
public enum AbilityType{Physical,Magical}