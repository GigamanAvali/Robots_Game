using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITableSelector : MonoBehaviour
{
	[SerializeField] private UIMainMenu mainMenu;
	[SerializeField] private AlembicPlayerController paperController;
	void Start()
	{
	}

	public void OnMouseOver()
	{
		//Debug.Log("OnMouseOver");
		paperController.Play(1.25f, 2.5f, true);
	}

	public void OnMouseExit()
	{
		paperController.Play(0, 1.25f, false);
	}

	public void OnMouseDown()
	{
		//Debug.Log("OnMouseDown");
		mainMenu.ShowStartPanel();
	}
}
