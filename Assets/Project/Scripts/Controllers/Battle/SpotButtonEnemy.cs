using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpotButtonEnemy : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
	public GameObject pointerImage;
	private GameObject p;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnPointerEnter(PointerEventData pd){
		if(p!= null){
			Destroy(p);
		}
		if(gameObject.GetComponent<Button>().interactable == true){
			if(p == null){
				p = Instantiate(pointerImage);
			}
			p.transform.SetParent(GameObject.Find(gameObject.GetComponent<BattleEnemyButtonController>().member.battleLocation + "Text").transform,false);
		}
	}
	public void OnPointerExit(PointerEventData pd){
		if(p!= null){
			Destroy(p);
		}
	}
}
