using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnequipItemButtonController : MonoBehaviour {

	private StatusMenuController stat;
	private EquipController eq;
	public GameObject invPanel;
	public Text invText;

	// Use this for initialization
	void Start () {
		stat = GameObject.Find("MenuControllers").GetComponent<StatusMenuController>();
		eq = GameObject.Find("ItemControllers").GetComponent<EquipController>();
		gameObject.transform.SetParent(invPanel.transform,false);
		Button theBtn = gameObject.GetComponent<Button>();
		theBtn.onClick.AddListener(SendToUse);
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Button>().interactable == false){
			invText.GetComponent<Text>().color = new Color(0.4f,0.4f,0.4f,1.0f);
		}
		else{
			invText.GetComponent<Text>().color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
	}

	private void SendToUse(){
		eq.Unequip();
		stat.HideChangeEquipMenu();
	}
}
