using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GenericButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {
	public Image hoverImage;
	public Button theBtn;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.GetComponent<Button>().interactable == false){
			for(int i = 0; i < gameObject.GetComponentsInChildren<Text>().Length;i++){
				((Text)gameObject.GetComponentsInChildren<Text>()[i]).color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
			}
		}
		else{
			for(int i = 0; i < gameObject.GetComponentsInChildren<Text>().Length;i++){
				((Text)gameObject.GetComponentsInChildren<Text>()[i]).color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
		}
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if(theBtn.interactable){
			hoverImage.gameObject.SetActive(true);
		}
	}
    public void OnPointerExit(PointerEventData eventData)
    {
    	hoverImage.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData) { }
    public void OnPointerDown(PointerEventData eventData)
    {
		if (theBtn.interactable)
		{
			hoverImage.transform.localPosition = new Vector3(hoverImage.transform.localPosition.x + 10, hoverImage.transform.localPosition.y, hoverImage.transform.localPosition.z);
		}
    }
    public void OnPointerUp(PointerEventData eventData)
    {
		if (theBtn.interactable)
		{
			hoverImage.transform.localPosition = new Vector3(hoverImage.transform.localPosition.x - 10, hoverImage.transform.localPosition.y, hoverImage.transform.localPosition.z);
		}
    }
	void OnDisable(){
		hoverImage.gameObject.SetActive(false);
	}
}
