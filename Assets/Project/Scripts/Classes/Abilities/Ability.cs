using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {

	public string abilityName;
	public string abilityDesc;
	public AbilityType abilityType;
	public bool useableOutOfCombat;
	public int mpCost;
	public int speedRank;
	public float damage;
	public float heal;
	public bool applyHaste;
	public int delay;
	public int atkDown;
	public int mgAtkDown;
	public int defDown;
	public int magDefDown;
	public int agiDown;
	public string defaultAttackAnimation;
	public TargetType targetType;

	// Use this for initialization
	public Ability(string name,string desc,AbilityType abilityType, bool useable, int mp, int speed, float dam, float hpup,bool haste, int dela, int atkdwn, int magakdwn, int defdwn, int magdefdwn, int agidwn,string anim,TargetType type){
		this.abilityName = name;
		this.abilityDesc = desc;
		this.abilityType = abilityType;
		this.useableOutOfCombat = useable;
		this.mpCost = mp;
		this.applyHaste = haste;
		this.speedRank = speed;
		this.damage = dam;
		this.heal = hpup;
		this.delay = dela;
		this.atkDown = atkdwn;
		this.mgAtkDown = magakdwn;
		this.defDown = defdwn;
		this.magDefDown = magdefdwn;
		this.agiDown = agidwn;
		this.defaultAttackAnimation = anim;
		this.targetType = type;
	}
}
