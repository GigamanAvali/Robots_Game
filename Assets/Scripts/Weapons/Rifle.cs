using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
	[SerializeField] private float atackTimeSec = 1;
	[SerializeField] private LaserSight laserSight;
	[SerializeField] private LaserSight2 laserSight2;
	[SerializeField] private Transform muzzle;
	[SerializeField] private AudioSource audioSource;
	private Rigidbody bullet = null;
	private AppData appData;

	private bool isShowed = false;
	private bool isAtacking = false;
	private int rayCount = 1;

	public virtual void Awake()
	{
		appData = GameObject.FindObjectOfType<AppData>();
		isAtacking = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4)) ShowWeapon();
		if (isShowed && Input.GetKey(KeyCode.Mouse0)) Attack();
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
	private void Attack()
	{
		if (appData == null)
			appData = GameObject.FindObjectOfType<AppData>();
		if (!isAtacking)
		{
			audioSource.Play();
			isAtacking = true;
			bullet = appData.GetBulletFromPool(muzzle.position + muzzle.forward);
			bullet.transform.LookAt(transform);
			Projectile bulletProjectile = bullet.GetComponent<Projectile>();
			bulletProjectile.Damage = 1;
			bullet.AddForce(muzzle.forward * 1.5f, ForceMode.Impulse);
			//bullet.AddForce(transform.up * 2f, ForceMode.Impulse);
			Invoke(nameof(ResetAtack), atackTimeSec);
		}
	}
	public virtual void ResetAtack()
	{
		isAtacking = false;
	}

}
