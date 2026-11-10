using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private GameObject hitPrefab;
	[SerializeField] private GameObject decalPrefab;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private int maxRicochet = 4;
	public Vector3 StartPos;
	private int curRicochet = 0;
	public int Damage = 1;
    private float curTime = 0;

    private void Start()
	{
	}

    private void Update()
    {
		UpdateTimer();
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
			//Vector3 rotatedNormal = Quaternion.AngleAxis(180f, axis) * collisionNormal;
			//GameObject decal = Instantiate(decalPrefab, transform.position, Quaternion.LookRotation(rotatedNormal));
			GameObject decal = Instantiate(decalPrefab, transform.position, Quaternion.LookRotation(collisionNormal));
			decal.SetActive(true);
			decal.transform.parent = collision.transform;
			Destroy(decal, 60);
		}
		if (curRicochet < maxRicochet)
		{
			curRicochet++;
			Vector3 incomingDirection = collision.contacts[0].point - StartPos; 
			Vector3 reflectedDir = Vector3.Reflect(incomingDirection.normalized, collisionNormal); 
			var bullet = GetComponent<Rigidbody>();
			bullet.velocity = Vector3.zero;
			bullet.AddForce(reflectedDir.normalized* 0.01f, ForceMode.Impulse);
			StartPos = transform.position;
			if (audioSource != null)
			{
				audioSource.Play();
			}
		}
		else
		{
			curRicochet = 0;
			gameObject.SetActive(false);
		}
		curTime = 0;
	}
	private void UpdateTimer()
	{
        curTime += Time.deltaTime;

        if (curTime > 10)
        {
			curTime = 0;
            gameObject.SetActive(false);


        }

	}
}
