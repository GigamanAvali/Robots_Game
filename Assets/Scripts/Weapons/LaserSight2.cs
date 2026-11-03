using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserSight2 : MonoBehaviour
{
	 
	[SerializeField] private Transform muzzle;
	[SerializeField] private LineRenderer trajectory;
	[SerializeField] private float sightRange = 100;
	[SerializeField] private GameObject laserDecal;
	private GameObject[] decals = new GameObject[4];
	private RaycastHit[] hits = new RaycastHit[4];
	private bool attackStart = false;
	private int rayCount = 1;


	private void OnDisable()
	{
		HideDevals();

    }
    private void Start()
    {
		CreateDecale();
    }

    private void Update()
	{
		//float glow = (Mathf.Sin(Time.time) + 1.0f) * 2.5f;
		//material.SetFloat("_GlowAmount", glow);

		//if (Input.GetKey(KeyCode.Mouse0)) Attack();
		CastRay();
	}
	
	private void CreateDecale()
	{
		for (int i = 0; i < decals.Length; i++)
		{
			var newDecale = Instantiate(laserDecal);
			//newDecale.transform.parent = transform;
			newDecale.SetActive(false);
			decals[i] = newDecale;
		}
	}

	private void HideDevals()
	{
		for(int i = 0;i < decals.Length;i++)
		{
			if (decals[i] != null)
			{
				decals[i].SetActive(false);
			}
		}
	}

	private void CastRay()
	{
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

			//decals[0].gameObject.SetActive(true);


			//decals[0].transform.position = hits[0].point + hits[0].normal * 0.01f;
			//decals[0].transform.rotation = Quaternion.LookRotation(hits[0].normal);

			//decals[0] = Instantiate(laserDecal, hits[0].point, Quaternion.identity);
			DecaleCreate(hits[0].point, hits[0].normal, 0);

            DrawRays();
		}
		else
		{
			Debug.DrawLine(muzzle.position, muzzle.position + muzzle.forward * sightRange, Color.red);
            DecaleCreate(false, 2);
            DecaleCreate(false, 1);
            DecaleCreate(false, 0);

        }
	}

	public void SetRayCount(int count)
	{
		if (count > 4)
			count = 4;
		rayCount = count;
		Debug.Log($"SetRayCount {rayCount}");
	}

	private void DrawRays()
	{
		//Debug.Log($"DrawRays {rayCount}");
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

            DecaleCreate(hits[1].point, hits[1].normal, 1);

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
				//Debug.DrawLine(hits[1].point, hits[1].point + reflectedDir1 * sightRange, Color.red);

                if (Physics.Raycast(hits[1].point, reflectedDir1, out hits[2], sightRange))
				{

					Debug.DrawLine(hits[1].point, hits[2].point, Color.red);
                    DecaleCreate(hits[2].point, hits[2].normal, 2);
				}
				else
				{
					Debug.DrawLine(hits[1].point, hits[1].point + reflectedDir1 * sightRange, Color.red);
                    DecaleCreate(false, 2);
                }

            }
			else
			{
				Debug.DrawLine(hits[0].point, hits[0].point + reflectedDir * sightRange, Color.red);
                DecaleCreate(false, 1);
                DecaleCreate(false, 2);
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
	private void DecaleCreate(Vector3 decalPosition, Vector3 decalNormal, int number)
	{
        decals[number].gameObject.SetActive(true);
        decals[number].transform.position = decalPosition + decalNormal * 0.01f;
        decals[number].transform.rotation = Quaternion.LookRotation(decalNormal);
    }
    private void DecaleCreate(bool status, int number)
    {
        decals[number].gameObject.SetActive(status);
    }

}
