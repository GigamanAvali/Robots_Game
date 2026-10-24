using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
	[SerializeField] private Transform muzzle;
	[SerializeField] private LineRenderer trajectory;
	[SerializeField] private GameObject decal;
	[SerializeField] private float sightRange = 100;
	private RaycastHit[] hits = new RaycastHit[4];
	private GameObject[] decals = new GameObject[4];
	private bool attackStart = false;
	private int rayCount = 1;

	private List<Vector3> points = new List<Vector3>();
	private void Start()
	{
		CreateDecals();
	}
	private void OnDisable()
	{
		points.Clear();
		points.Add(muzzle.position);
		UpdateTrajectory(points.ToArray());
		HideDecals();
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
		points.Clear();
		points.Add(muzzle.position);
		Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit1, sightRange);
		if (Physics.Raycast(muzzle.position, muzzle.forward, out hits[0], sightRange))
		{
			//Debug.Log("hit " + hit[0].transform.name);
			attackStart = true;
			Vector3 hitNormal = hits[0].normal; // Нормаль от точки попадания
			Vector3 hitPoint = hits[0].point; // Нормаль от точки попадания
			Vector3 incomingDirection = hitPoint - muzzle.position; // Входящий вектор 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, hitNormal); // Отраженный вектор
			//Самый первый вектор из дула в первую точку попадания
			//Debug.DrawLine(muzzle.position, hits[0].point, Color.red);
			decals[0].SetActive(true);
			decals[0].transform.position = hitPoint + hitNormal * 0.05f;
			decals[0].transform.rotation = Quaternion.LookRotation(hitNormal);
			points.Add(hitPoint);
			//UpdateTrajectory(points.ToArray());
			//DrawRays(rayCount, reflectedDir);
			
			DrawRays();
		}
		else
		{
			decals[0].SetActive(false);
			//Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward * sightRange, Color.red);
			points.Add(muzzle.position + muzzle.forward * sightRange);
			UpdateTrajectory(points.ToArray());
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
			UpdateTrajectory(points.ToArray());
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
				//Debug.DrawLine(hits[0].point, hits[1].point, Color.red);
				points.Add(hits[1].point);
				if (rayCount == 2)
				{
					UpdateTrajectory(points.ToArray());
					return;
				}
					
				Vector3 incomingDirection1 = hits[1].point - hits[0].point; // Второй входящий вектор 
				// Второй отражённый луч
				Vector3 reflectedDir1 = Vector3.Reflect(incomingDirection1.normalized, hits[1].normal);
				//Debug.DrawLine(hits[1].point, hits[1].point + reflectedDir1 * sightRange, Color.red);
				points.Add(hits[1].point + reflectedDir1 * sightRange);
				UpdateTrajectory(points.ToArray());
			}
			else
			{
				//Debug.DrawLine(hits[0].point, hits[0].point + reflectedDir * sightRange, Color.red);
				points.Add(hits[0].point + reflectedDir * sightRange);
				UpdateTrajectory(points.ToArray());
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


	private void UpdateTrajectory(Vector3[] points)
	{
		//Debug.Log(points.Length);
		trajectory.SetPositions(points);
		trajectory.positionCount = points.Length;
	}

	private void CreateDecals()
	{
		for (int i = 0; i < decals.Length; i++)
		{
			var newDecal = Instantiate(decal);
			newDecal.SetActive(false);
			decals[i] = newDecal;
		}
	}

	private void HideDecals()
	{
		for (int i = 0; i < decals.Length; i++)
		{
			decals[i]?.SetActive(false);
		}
	}
}
