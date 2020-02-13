using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour {

	public GameObject settingsPanel;
	public Resolution[] resolutions;
	public Toggle fullScreenToggle;
	public Dropdown resolutionDropdown;
	private MainMenuController main;
	private List<string> reslist;

	// Use this for initialization
	void Start () {
		main = GameObject.Find("MenuControllers").GetComponent<MainMenuController>();
		reslist = new List<string>(){"640 x 360","960 x 540","1280 x 720","1366 x 768","1600 x 900","1664 x 936","1920 x 1080","2560 x 1440","3200 x 1800","3840 x 2160", "4096 x 2304"};
		AddOptions();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void AddOptions(){
		resolutionDropdown.ClearOptions();
		resolutionDropdown.AddOptions(reslist);
	}

	public bool SetPause(){
		if(settingsPanel.activeSelf){
			HideSettingsPanel();
			return true;
		}
		return false;
	}

	public void ShowSettingsPanel(){
		settingsPanel.SetActive(true);
		main.HideMainPanel();
		AddOptions();
		UpdateOptions();
	}
	public void HideSettingsPanel(){
		settingsPanel.SetActive(false);
		main.ShowMainPanel();
	}
	public void ApplySettings(){
		GameSettings.resolutionIndex = resolutionDropdown.value;
		GameSettings.fullscreen = Screen.fullScreen = fullScreenToggle.isOn;
		Screen.SetResolution(int.Parse(reslist[GameSettings.resolutionIndex].Split('x')[0]), int.Parse(reslist[GameSettings.resolutionIndex].Split('x')[1]), GameSettings.fullscreen);
	}
	public void UpdateOptions(){
		fullScreenToggle.isOn = GameSettings.fullscreen;
		resolutionDropdown.value = GameSettings.resolutionIndex;
	}
}
