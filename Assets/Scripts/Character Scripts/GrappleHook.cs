using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GrappleHook : MonoBehaviour
{
    //assign
    public Camera camera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public LayerMask ceiling;
    //assign

    public bool isCeiling = false;
    public bool isPulled = false;
    public bool isGrappled;
    public Vector2 mousePosition;
    
    void Start()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);

            isCeiling = Physics2D.OverlapCircle(mousePosition, 0.1f, ceiling);
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            isGrappled = false;
            isCeiling = false;
        }
        
        if (isCeiling)
        {
            distanceJoint.connectedAnchor = mousePosition;
            distanceJoint.enabled = true;
            
            lineRenderer.SetPosition(1,transform.position);
            lineRenderer.SetPosition(0, mousePosition);
            lineRenderer.enabled = true;
        }

        if (distanceJoint.enabled)
        {
            isGrappled = true;
            lineRenderer.SetPosition(1, transform.position);

            if (Input.GetMouseButtonDown(1))
            {
                isPulled = true;
                distanceJoint.autoConfigureDistance = false;
            }

            if (isPulled)
            {
                distanceJoint.distance -= 20f * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(1))
            {
                isPulled = false;
                distanceJoint.autoConfigureDistance = true;
            }
        }
    }
}
