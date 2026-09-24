using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
	[SerializeField] private LaserSight laserSight;
	private bool isShowed = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4)) ShowWeapon();
	}

	private void ShowWeapon()
	{
		Debug.Log("ShowWeapon");
		isShowed = !isShowed;
		laserSight.gameObject.SetActive(isShowed);
	}

}
