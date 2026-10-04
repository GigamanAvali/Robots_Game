using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    private LineRenderer lineRenderer;
    void Start()
    {
       lineRenderer = GetComponent<LineRenderer>(); 
    }

    public void UpdateTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        int lastCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + 0 * Physics.gravity * time * time / 2;
            if (points[i].y < 0)
            {
                lastCount = i + 1;
                break;
            }
        }
        lineRenderer.SetPositions(points);
        lineRenderer.positionCount = lastCount;
    }
    void Update()
    {
        
    }
}
