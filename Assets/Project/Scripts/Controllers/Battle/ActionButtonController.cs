using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonController : MonoBehaviour {
	public UnitStats member;
	public Action action;
	public int loc;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Button>().onClick.AddListener(InitiateAction);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateText()
    {
        ((Text)gameObject.transform.GetComponentInChildren<Text>()).text = action.ToString();
    }
    public void AddParent(GameObject parentPanel)
    {
        gameObject.transform.SetParent(parentPanel.transform, false);
    }
	public void InitiateAction(){
		switch(action){
			case Action.Mode:
				break;
			default:
				break;
		}
	}
}
