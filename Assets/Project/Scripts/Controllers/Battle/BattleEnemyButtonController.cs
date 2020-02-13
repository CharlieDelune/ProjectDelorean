using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnemyButtonController : MonoBehaviour {

	public UnitStats member;
	public int loc;
	private TargetHandler target;
	// Use this for initialization
	void Start () {
		target = GameObject.Find("BattleControllers").GetComponent<TargetHandler>();
		gameObject.GetComponent<Button>().onClick.AddListener(SendToTargetHandler);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateText(){
		((Text)gameObject.transform.Find("MemberName").GetComponent("Text")).text = member.charName;
	}
	public void AddParent(GameObject parentPanel){
		gameObject.transform.SetParent(parentPanel.transform, false);
	}
	void SendToTargetHandler(){
		target.SetTarget(member);
	}
}
