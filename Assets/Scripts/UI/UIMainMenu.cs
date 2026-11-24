using Project.Core.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
	[SerializeField] private GameObject startPanel;
	[SerializeField] private GameObject profilePanel;
	private bool isPanelOpened = false;
	public bool IsPanelOpened
	{
		get 
		{
			return isPanelOpened;
		}
	}
	void Start()
	{
	}

	void Update()
	{
	}

	public void ChangeLanguage()
	{
		Localization.Instance.ChangeLanguage();
		//Debug.Log(Localization.Instance.Language);
	}

	public void ShowStartPanel()
	{
		if (isPanelOpened)
			return;
		startPanel.SetActive(true);
		isPanelOpened = true;
	}

	public void HideStartPanel()
	{
		startPanel.SetActive(false);
		isPanelOpened = false;
	}

	public void ShowProfilePanel()
	{
		if (isPanelOpened)
			return;
		profilePanel.SetActive(true);
		isPanelOpened = true;
	}

	public void HideProfilePanel()
	{
		profilePanel.SetActive(false);
		isPanelOpened = false;
	}

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void LoadMenuScene()
	{
		SceneManager.LoadScene("MenuScene");
	}
}