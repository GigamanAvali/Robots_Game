using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{

	[SerializeField] private Transform muzzle;
	[SerializeField] private float sightRange = 100;
	private RaycastHit[] hit = new RaycastHit[4];
	private bool attackStart = false;

	private void Update()
	{
		//if (Input.GetKey(KeyCode.Mouse0)) Attack();
		CastRay();
	}

	private void CastRay()
	{
		if (Physics.Raycast(muzzle.position, muzzle.forward, out hit[0], sightRange))
		{
			attackStart = true;
			//Debug.Log("hit " + hit[0].transform.name);
			Vector3 incomingDirection = hit[0].point - muzzle.position;
			//Debug.DrawRay(
			//		hit.point,
			//		incomingDirection * 5f,
			//		Color.red,
			//		2f
			//	);
		}
	}

	private void OnDrawGizmos()
	{
		if (attackStart)
		{
			Gizmos.color = Color.yellow;
			// Первая нормаль
			Vector3 hitNormal = hit[0].normal; // Нормаль от точки попадания
			//Gizmos.DrawLine(hit[0].point, hit[0].point + hitNormal);
			Gizmos.color = Color.red;
			// Первый луч
			Gizmos.DrawLine(muzzle.position, hit[0].point);
			Vector3 incomingDirection = hit[0].point - muzzle.position; // Входящий вектор 
			// Отраженный вектор 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, hitNormal);

			if (Physics.Raycast(hit[0].point, reflectedDir, out hit[1], sightRange))
			{
				Vector3 hitNormal1 = hit[1].normal;
				Gizmos.color = Color.yellow;
				//Gizmos.DrawLine(hit[1].point, hit[1].point + hitNormal);
				Gizmos.color = Color.red;
				Gizmos.DrawLine(hit[0].point, hit[1].point);
				Vector3 incomingDirection1 = hit[0].point - hit[1].point;
				Vector3 reflectedDir1 = Vector3.Reflect(incomingDirection1.normalized, hitNormal1);
				Gizmos.DrawLine(hit[1].point, hit[1].point + reflectedDir1 * sightRange);
			}
			else
			{
				Gizmos.DrawLine(hit[0].point, hit[0].point + reflectedDir * sightRange);

			}
		}
	}

}
