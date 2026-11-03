using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private GameObject hitPrefab;
	[SerializeField] private GameObject decalPrefab;
	public int Damage = 1;

	private void Start()
	{
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("OnCollisionEnter " + collision.gameObject.name);
		BaseEntity entity = collision.gameObject.GetComponent<BaseEntity>();
		if (entity != null)
		{
			entity.Health -= Damage;
		}
		Vector3 collisionNormal = collision.contacts[0].normal;
		if (hitPrefab != null)
		{
			GameObject hit = Instantiate(hitPrefab, transform.position, Quaternion.LookRotation(collisionNormal));
			Destroy(hit, 5);
		}
		//if (!collision.transform.name.Contains("Enemy") && !collision.transform.name.Contains("Player"))
		//{
		//}
		if (decalPrefab != null)
		{
			//GameObject decal = Instantiate(decalPrefab, transform.position, Quaternion.LookRotation(collisionNormal));
			//если материал односторонний нужно развернуть на 180 градусов
			Vector3 axis = Vector3.up; // Ось вращения (Y)
			Vector3 rotatedNormal = Quaternion.AngleAxis(180f, axis) * collisionNormal;
			GameObject decal = Instantiate(decalPrefab, transform.position, Quaternion.LookRotation(rotatedNormal));
			decal.SetActive(true);
			decal.transform.parent = collision.transform;
			Destroy(decal, 60);
		}

		//Destroy(gameObject);
		gameObject.SetActive(false);
	}
}
