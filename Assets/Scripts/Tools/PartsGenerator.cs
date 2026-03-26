using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsGenerator : MonoBehaviour
{
	[Header("Bones")]
	[Header("Right leg")]
	[SerializeField] private Transform rightFootParent;
	[SerializeField] private Transform rightKneeParent;
	[SerializeField] private Transform rightHipParent;
	[Header("Left leg")]
	[SerializeField] private Transform leftFootParent;
	[SerializeField] private Transform leftKneeParent;
	[SerializeField] private Transform leftHipParent;
	[Header("Pelvis")]
	[SerializeField] private Transform pelvisParent;
	[Header("Right arm")]
	[SerializeField] private Transform rightPalmParent;
	[SerializeField] private Transform rightForeArmParent;
	[SerializeField] private Transform rightShoulderParent;
	[Header("Left arm")]
	[SerializeField] private Transform leftPalmParent;
	[SerializeField] private Transform leftForeArmParent;
	[SerializeField] private Transform leftShoulderParent;
	[Header("Chest")]
	[SerializeField] private Transform chest;
	[Header("Head")]
	[SerializeField] private Transform head;

	[Header("Robot parts ")]
	[Header("Right leg")]
	[SerializeField] private GameObject[] rightFoots;
	[SerializeField] private GameObject[] rightKnees;
	[SerializeField] private GameObject[] rightHips;
	[Header("Left leg")]
	[SerializeField] private GameObject[] leftFoots;
	[SerializeField] private GameObject[] leftKnees;
	[SerializeField] private GameObject[] leftHips;
	[Header("Pelvis")]
	[SerializeField] private GameObject[] pelvises;
	[SerializeField] private GameObject mainPelvis;
	[Header("Right arm")]
	[SerializeField] private GameObject[] rightPalms;
	[SerializeField] private GameObject[] rightForeArms;
	[SerializeField] private GameObject[] rightShoulders;
	[Header("Left arm")]
	[SerializeField] private GameObject[] leftPalms;
	[SerializeField] private GameObject[] leftForeArms;
	[SerializeField] private GameObject[] leftShoulders;
	[Header("Chest")]
	[SerializeField] private GameObject[] chests;
	[Header("Head")]
	[SerializeField] private GameObject[] heads;


	void Start()
	{
		GenerateParts();
	}

	void Update()
	{
		
	}

	private void GenerateParts()
	{
		CreatePart(rightFootParent, rightFoots);
		CreatePart(rightKneeParent, rightKnees);
		CreatePart(rightHipParent, rightHips);

		CreatePart(leftFootParent, leftFoots);
		CreatePart(leftKneeParent, leftKnees);
		CreatePart(leftHipParent, leftHips);

		CreatePart(pelvisParent, pelvises);
		CreatePart(pelvisParent, mainPelvis);

		CreatePart(rightPalmParent, rightPalms);
		CreatePart(rightForeArmParent, rightForeArms);
		CreatePart(rightShoulderParent, rightShoulders);

		CreatePart(leftPalmParent, leftPalms);
		CreatePart(leftForeArmParent, leftForeArms);
		CreatePart(leftShoulderParent, leftShoulders);

		CreatePart(chest, chests);

		CreatePart(head, heads);
	}
	private void CreatePart(Transform partsBone, GameObject[] parts)
	{
		Vector3 curPos = transform.position;
		GameObject basePartRnd = parts[Random.Range(0, parts.Length)];
		GameObject newPart = Instantiate(basePartRnd, curPos, basePartRnd.transform.rotation);
		newPart.transform.parent = partsBone;
	}
	private void CreatePart(Transform partsBone, GameObject part)
	{
		Vector3 curPos = transform.position;
		GameObject newPart = Instantiate(part, curPos, part.transform.rotation);
		newPart.transform.parent = partsBone;
	}
}
