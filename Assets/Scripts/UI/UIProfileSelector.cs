using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProfileSelector : MonoBehaviour
{
	[SerializeField] private UIMainMenu mainMenu;
	[SerializeField] private Camera camera;
	[SerializeField] private Animator animator;
	[SerializeField] private float rotateStrength = 10;
	private bool isNeedRotate = false;
	private bool isExiting = false;
	private Quaternion startRotation, targetRotation;
	void Start()
	{
		startRotation = transform.rotation;
	}
	void Update()
	{
		if (isNeedRotate)
			RotateTowardsTarget();
	}
	public void OnMouseOver()
	{
        if (mainMenu.IsPanelOpened)
            return;
        isNeedRotate = true;
		Vector3 direction = camera.transform.position - transform.position;
		direction.y = 0f; // Убираем наклон вверх/вниз
		targetRotation = Quaternion.LookRotation(direction);
		//transform.rotation = Quaternion.Euler(0, 195, 0);
	}

	public void OnMouseExit()
	{
		if (mainMenu.IsPanelOpened)
			return;
		StopDance();
	}

	public void OnMouseDown()
	{
		//Debug.Log("OnMouseDown");
		mainMenu.ShowProfilePanel();
	}

	private void RotateTowardsTarget()
	{
		//Debug.Log("rotating");
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateStrength * Time.deltaTime);

		if (Quaternion.Angle(transform.rotation, targetRotation) < 0.0001f)
		{
			//Debug.Log("The Rotating Has Completed");
			transform.rotation = targetRotation;
			isNeedRotate = false;
			if (!isExiting)
			{
				animator.SetBool("IsDance", true);
			}
			isExiting = false;
		}
	}

	public void StopDance()
	{
		isNeedRotate = true;
		isExiting = true;
		targetRotation = startRotation;
		animator.SetBool("IsDance", false);
		//transform.rotation = Quaternion.Euler(0, 300, 0);
	}
}
