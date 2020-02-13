using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattlePartyButtonController : MonoBehaviour
{

    public UnitStats member;
    public int loc;
    public Sprite attackSprite;
    public Sprite defenseSprite;
    public TargetHandler target;
    public Image hoverImage;
    public Button theBtn;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("BattleControllers").GetComponent<TargetHandler>();
        gameObject.GetComponent<Button>().onClick.AddListener(SendToTargetHandler);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<Button>().interactable == false){
            ((Text)gameObject.transform.Find("MemberName").GetComponent("Text")).color = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        }
        else{
            ((Text)gameObject.transform.Find("MemberName").GetComponent("Text")).color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
    }

    public void UpdateText()
    {
        ((Text)gameObject.transform.Find("MemberName").GetComponent("Text")).text = member.charName;
        gameObject.transform.Find("MemberHPBarHolder").transform.Find("MemberHPBar").GetComponent("Image").transform.localScale = new Vector3((float)member.currentHealth / (float)member.maxHealth, gameObject.transform.Find("MemberHPBarHolder").transform.Find("MemberHPBar").GetComponent("Image").transform.localScale.y, gameObject.transform.Find("MemberHPBarHolder").transform.Find("MemberHPBar").GetComponent("Image").transform.localScale.z);
        gameObject.transform.Find("MemberMPBarHolder").transform.Find("MemberMPBar").GetComponent("Image").transform.localScale = new Vector3((float)member.currentMana / (float)member.maxMana, gameObject.transform.Find("MemberMPBarHolder").transform.Find("MemberMPBar").GetComponent("Image").transform.localScale.y, gameObject.transform.Find("MemberMPBarHolder").transform.Find("MemberMPBar").GetComponent("Image").transform.localScale.z);
        ((Text)gameObject.transform.Find("MemberHPBarHolder").transform.Find("MemberHPHolder").transform.Find("MemberHPCurrValue").GetComponent("Text")).text = member.currentHealth.ToString();
        ((Text)gameObject.transform.Find("MemberHPBarHolder").transform.Find("MemberHPHolder").transform.Find("MemberHPMaxValue").GetComponent("Text")).text = member.maxHealth.ToString();
        ((Text)gameObject.transform.Find("MemberMPBarHolder").transform.Find("MemberMPHolder").transform.Find("MemberMPCurrValue").GetComponent("Text")).text = member.currentMana.ToString();
        ((Text)gameObject.transform.Find("MemberMPBarHolder").transform.Find("MemberMPHolder").transform.Find("MemberMPMaxValue").GetComponent("Text")).text = member.maxMana.ToString();
        if (member.charMode == Mode.Attack)
        {
            ((Image)gameObject.transform.Find("MemberModeIndicator").GetComponent("Image")).sprite = attackSprite;
        }
        else if (member.charMode == Mode.Defense)
        {
            ((Image)gameObject.transform.Find("MemberModeIndicator").GetComponent("Image")).sprite = defenseSprite;
        }
    }
    public void AddParent(GameObject parentPanel)
    {
        gameObject.transform.SetParent(parentPanel.transform, false);
    }
    void SendToTargetHandler(){
        target.SetTarget(member);
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
