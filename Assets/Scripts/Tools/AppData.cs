using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppData : MonoBehaviour
{
	public Transform Player;
	[SerializeField] private Transform bulletProjectile;
	private List<Rigidbody> bulletsPool = new List<Rigidbody>();
	public Camera PlayerCamera;

	public Rigidbody GetBulletFromPool(Vector3 position)
	{
		foreach (var item in bulletsPool)
		{
			if (!item.gameObject.activeInHierarchy)
			{
				item.velocity = Vector3.zero;
				item.transform.position = position;
				item.gameObject.SetActive(true);
				return item;
			}
		}
		Rigidbody bullet = Instantiate(bulletProjectile, position, Quaternion.identity).gameObject.GetComponent<Rigidbody>();
		bullet.gameObject.transform.parent = transform;
		bullet.gameObject.SetActive(true);
		bulletsPool.Add(bullet);
		return bullet;
	}
}
