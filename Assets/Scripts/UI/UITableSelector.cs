using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITableSelector : MonoBehaviour
{
	[SerializeField] private UIMainMenu mainMenu;
	void Start()
	{
	}

	public void OnMouseOver()
	{
	}

	public void OnMouseExit()
	{
	}

	public void OnMouseDown()
	{
		//Debug.Log("OnMouseDown");
		mainMenu.ShowStartPanel();
	}
}
