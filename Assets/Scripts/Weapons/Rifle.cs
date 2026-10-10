using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
	[SerializeField] private LaserSight1 laserSight;
	private bool isShowed = false;
	private int rayCount = 1;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4)) ShowWeapon();
	}

	private void ShowWeapon()
	{
		//Debug.Log("ShowWeapon");
		if (isShowed)
		{
			rayCount++;
			if (rayCount > 3)
			{
				isShowed = false;
				laserSight.gameObject.SetActive(isShowed);
				rayCount = 1;
				return;
			}
			laserSight.SetRayCount(rayCount);
			
		}
		else
		{
			isShowed = true;
			laserSight.gameObject.SetActive(isShowed);
			laserSight.SetRayCount(rayCount);
		}
	}

}
