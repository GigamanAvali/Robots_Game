using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameMenu : MonoBehaviour
{
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private GameObject gamePanel;
	[SerializeField] private GameObject loosePanel;

	void Start()
	{
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			ShowMenu();
		}

	}

	private void ShowMenu()
	{
		FirstPerson.IsActive = false;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		menuPanel.SetActive(true);
	}

	public void HideMenu()
	{
		FirstPerson.IsActive = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		menuPanel.SetActive(false);
	}
}