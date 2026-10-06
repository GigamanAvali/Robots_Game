using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight1 : MonoBehaviour
{
	 
	[SerializeField] private Transform muzzle;
	[SerializeField] private LineRenderer trajectory;
	[SerializeField] private float sightRange = 100;
	private RaycastHit[] hits = new RaycastHit[4];
	private bool attackStart = false;
	private int rayCount = 1;


	private void OnDisable()
	{

	}

	private void Update()
	{
		//float glow = (Mathf.Sin(Time.time) + 1.0f) * 2.5f;
		//material.SetFloat("_GlowAmount", glow);

		//if (Input.GetKey(KeyCode.Mouse0)) Attack();
		CastRay();
	}

	private void CastRay()
	{
		Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit1, sightRange);
		if (Physics.Raycast(muzzle.position, muzzle.forward, out hits[0], sightRange))
		{
			//Debug.Log("hit " + hit[0].transform.name);
			attackStart = true;
			Vector3 hitNormal = hits[0].normal; // Нормаль от точки попадания
			Vector3 incomingDirection = hits[0].point - muzzle.position; // Входящий вектор 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, hitNormal); // Отраженный вектор
			//Самый первый вектор из дула в первую точку попадания
			Debug.DrawLine(muzzle.position, hits[0].point, Color.red);
			
			
			//UpdateTrajectory(points.ToArray());
			//DrawRays(rayCount, reflectedDir);
			
			DrawRays();
		}
		else
		{
			Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward * sightRange, Color.red);
		}
	}

	public void SetRayCount(int count)
	{
		if (count > 4)
			count = 4;
		rayCount = count;

	}

	private void DrawRays()
	{
		if (rayCount == 1)
		{
			return;
		}
		//Debug.Log(rayCount);
		if (attackStart)
		{
			// Первая нормаль
			Vector3 hitNormal = hits[0].normal; // Нормаль от точки попадания
			//Debug.DrawLine(hits[0].point, hits[0].point + hitNormal, Color.yellow);
			Vector3 incomingDirection = hits[0].point - muzzle.position; // Входящий вектор 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, hitNormal); // Отраженный вектор
			if (Physics.Raycast(hits[0].point, reflectedDir, out hits[1], sightRange))
			{
				// Нормаль второй поверхности
				//Debug.DrawLine(hits[1].point, hits[1].point + hits[1].normal, Color.yellow);
				// Отражённый луч
				Debug.DrawLine(hits[0].point, hits[1].point, Color.red);
				if (rayCount == 2)
				{
					return;
				}
					
				Vector3 incomingDirection1 = hits[1].point - hits[0].point; // Второй входящий вектор 
				// Второй отражённый луч
				Vector3 reflectedDir1 = Vector3.Reflect(incomingDirection1.normalized, hits[1].normal);
				Debug.DrawLine(hits[1].point, hits[1].point + reflectedDir1 * sightRange, Color.red);
			}
			else
			{
				Debug.DrawLine(hits[0].point, hits[0].point + reflectedDir * sightRange, Color.red);
			}
		}
	}
	private void OnDrawGizmos()
	{

	}
	private void DrawRays(int count, Vector3 incomingDirection)
	{
		if (count >= hits.Length)
			return;
		if (Physics.Raycast(hits[count - 1].point, incomingDirection, out hits[count], sightRange))
		{
			Vector3 hitNormal = hits[count].normal;
			Debug.DrawLine(hits[count].point, hits[count].point + hitNormal, Color.yellow);
			Debug.DrawLine(hits[count - 1].point, hits[count].point, Color.red);
			Vector3 incomingDirection1 = hits[count - 1].point - hits[count].point;
			Vector3 reflectedDir1 = Vector3.Reflect(incomingDirection1.normalized, hitNormal);
			DrawRays(count + 1, reflectedDir1);

		}
	}

}
