using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{

	[SerializeField] private Transform muzzle;
	[SerializeField] private float sightRange = 100;
	private RaycastHit hit, hit1;
	private bool attackStart = false;

	private void Update()
	{
		if (Input.GetKey(KeyCode.Mouse0)) Attack();
	}

	private void Attack()
	{
		if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, sightRange))
		{
			attackStart = true;
			Debug.Log("hit " + hit.transform.name);

		}
	}

	private void OnDrawGizmos()
	{
		if (attackStart)
		{
			Gizmos.color = Color.yellow;
			// Первая нормаль
			Vector3 hitNormal = hit.point + hit.normal; // Нормаль от точки попадания
			Gizmos.DrawLine(hit.point, hitNormal);
			Gizmos.color = Color.red;
			// Первый луч
			Gizmos.DrawLine(muzzle.position, hit.point);
			Vector3 incomingDirection = hit.point - muzzle.position; // Входящий вектор 
			// Отраженный вектор 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, hitNormal);


			if (Physics.Raycast(hit.point, reflectedDir, out hit1, sightRange))
			{
				Vector3 hitNormal1 = hit1.point + hit1.normal;
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(hit1.point, hitNormal1);
				Gizmos.color = Color.red;
				Gizmos.DrawLine(hit.point, hit1.point);
			}
			else
			{
				Gizmos.DrawLine(hit.point, reflectedDir * 2);
			}
		}
	}

}
