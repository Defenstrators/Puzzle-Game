using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    
    LineRenderer lr;
    Vector3 grapplePoint;
    public LayerMask grappleLayer;

    public GameObject muzzlePoint;

    public float distance;
    public GameObject player;
    SpringJoint joint;

    public float minDistance;
    public float maxDistance;

  
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Grapple();
        }
        else if(Input.GetMouseButtonDown(1))
        {
            StopGrapple();
        }
    }

    void Grapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(muzzlePoint.transform.position, muzzlePoint.transform.forward, out hit, distance, grappleLayer))
        {
            joint = player.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = hit.point;

            float distanceFromPoint = Vector3.Distance(transform.position, hit.point);

            joint.maxDistance = distanceFromPoint * maxDistance;
            joint.minDistance = distanceFromPoint * minDistance;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;
        }
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
