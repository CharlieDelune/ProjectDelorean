using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenMemberController : MonoBehaviour {
	public UnitStats member;
	public Image head;
	public Text memberName;
	public Text level;
	public Text currentExp;
	public Text nextLevelExp;
	public Text hpValue;
	public Text mpValue;
	public Text atkValue;
	public Text atkPlusValue;
	public Text magAtkValue;
	public Text magAtkPlusValue;
	public Text defValue;
	public Text defPlusValue;
	public Text magDefValue;
	public Text magDefPlusValue;
	public Text agiValue;
	public Text agiPlusValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetParameters(GameObject go, UnitStats m){
		member = m;
		gameObject.transform.SetParent(go.transform);
		gameObject.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,gameObject.transform.localPosition.y,0.0f);
		gameObject.transform.localRotation = new Quaternion();
		head.sprite = member.battleHead;
		memberName.text = member.charName;
		RefreshStats();
	}

	public void RefreshStats(){
		level.text = member.level.ToString();
		hpValue.text = member.maxHealth.ToString();
		mpValue.text = member.maxMana.ToString();
		currentExp.text = member.experience.ToString();
		nextLevelExp.text = member.experienceToNextLevel.ToString();
		atkValue.text = member.baseAttack.ToString();
		magAtkValue.text = member.baseMagAttack.ToString();
		defValue.text = member.baseDefense.ToString();
		magDefValue.text = member.baseMagDefense.ToString();
		agiValue.text = member.baseAgility.ToString();
	}
}
