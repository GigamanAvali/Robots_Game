using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform gunpoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private int power = 2;
    private Camera camera;
    private Ray screenRay;

    private Rigidbody buller = null;

    void Start()
    {
        //camera = Camera.main;
    }


    void Update()
    {
        Attack();
    }

    //private void Attack()
    //{
    //    screenRay = camera.ScreenPointToRay(Input.mousePosition);
    //    //Debug.Log(Input.mousePosition);
    //    Vector3 endPosition = screenRay.GetPoint(8);
    //    endPosition.z = 0;
    //    Vector3 speedVector = endPosition - gunpoint.position;
    //    float distance = speedVector.magnitude;
    //    //Debug.Log(distance);
    //    gunpoint.rotation = Quaternion.LookRotation(speedVector);

    //    //speedVector *= power;
    //    trajectory.UpdateTrajectory(gunpoint.position + gunpoint.forward, speedVector);
    //    //if (Input.GetMouseButtonDown(0))
    //    //{ 
    //    // buller = Instantiate(projectile, gunpoint.position + gunpoint.forward, Quaternion.identity).gameObject.GetComponent<Rigidbody>();
    //    // buller.gameObject.SetActive(true);
    //    // buller.AddForce(speedVector, ForceMode.VelocityChange);
    //    // }
    //}

    private void Attack()
    {
        Ray screenRay = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenRay, out RaycastHit hit))
        {
            Vector3 target = hit.point;

            Vector3 direction = target - gunpoint.position;

            gunpoint.rotation = Quaternion.LookRotation(direction);

            trajectory.UpdateTrajectory(
                gunpoint.position + gunpoint.forward,
                gunpoint.forward
            );
        }
    }
}
