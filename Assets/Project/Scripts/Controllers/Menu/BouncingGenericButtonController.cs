using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BouncingGenericButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler, IPointerDownHandler, IPointerUpHandler {
	public Image hoverImage;
	public Button theBtn;
	public Vector3 initialPosition;
	// Use this for initialization
	void Start () {
		initialPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		if(theBtn.interactable){
			StartCoroutine("Bounce");
		}
	}
    public void OnPointerExit(PointerEventData eventData)
    {
    	StopCoroutine("Bounce");
		transform.localPosition = initialPosition;
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
		StopCoroutine(Bounce());
		transform.localPosition = initialPosition;
	}
	private IEnumerator Bounce(){
		while(true){
            float t = 0;
            while (t < 0.33){
                hoverImage.transform.localPosition += new Vector3(20 * Time.deltaTime, 0.0f, 0.0f);
                t += Time.deltaTime;
                yield return 0;
            }
			while (t < 0.66f){
                hoverImage.transform.localPosition -= new Vector3(20 * Time.deltaTime, 0.0f, 0.0f);
                t += Time.deltaTime;
                yield return 0;
            }
		}
	}
}
