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
	}
	private void CreatePart(Transform partsBone, GameObject[] parts)
	{
		Vector3 curPos = transform.position;
		GameObject basePartRnd = parts[Random.Range(0, parts.Length)];
		GameObject newPart = Instantiate(basePartRnd, curPos, basePartRnd.transform.rotation);
		newPart.transform.parent = partsBone;
	}
}
