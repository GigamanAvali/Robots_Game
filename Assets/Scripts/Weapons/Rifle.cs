using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField]
    private Transform aim;

   
    private void OnDrawGizmos()
    {
		//Gizmos.color = Color.red;
		//Gizmos.DrawSphere(aim.position, 0.1f);
		//Gizmos.color = Color.magenta;
	}

}
