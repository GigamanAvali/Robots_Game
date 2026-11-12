using Project.Core.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{

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

	public void LoadGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}

	public void LoadMenuScene()
	{
		SceneManager.LoadScene("MenuScene");
	}
}