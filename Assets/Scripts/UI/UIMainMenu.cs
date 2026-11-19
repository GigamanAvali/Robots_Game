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
		startPanel.SetActive(true);
	}

	public void HideStartPanel()
	{
		startPanel.SetActive(false);
	}

	public void ShowProfilePanel()
	{
		profilePanel.SetActive(true);
	}

	public void HideProfilePanel()
	{
		profilePanel.SetActive(false);
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