using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private float maxDistance = 200f;
    private SpringJoint joint;
    public LayerMask grappleable;
    public Transform hookTip, cam, pl;



    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //StartGrapple();
            StartGrappleSphereCast();
            //StartGrappleCapsuleCast();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        Debug.DrawRay(transform.position, cam.forward);
        DrawRope();
    }

    //simple raycast that basically just checks for a straight line between the objects and the camera
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappleable))
        {
            grapplePoint = hit.point;
            joint = pl.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distFromPt = Vector3.Distance(pl.position, grapplePoint);

            joint.maxDistance = distFromPt * 0.8f;
            joint.minDistance = distFromPt * 0.25f;

            // may need to alter for best fit
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void StartGrappleCapsuleCast()
    {
        RaycastHit hit;
        Vector3 distance;
        distance = cam.position + Vector3.forward * 200f;
        Debug.Log(cam.position);
        Debug.Log(distance);
        if (Physics.CapsuleCast(cam.position, distance, 10f, cam.forward, out hit, grappleable))
        {
            grapplePoint = hit.point;
            joint = pl.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distFromPt = Vector3.Distance(pl.position, grapplePoint);

            joint.maxDistance = distFromPt * 0.8f;
            joint.minDistance = distFromPt * 0.25f;

            // may need to alter for best fit
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void StartGrappleSphereCast()
    {
        RaycastHit hit;

        Vector3 origin = pl.position;

        origin.y += 5f;

        if (Physics.SphereCast(origin, 5f, cam.forward, out hit, maxDistance, grappleable))
        {
            grapplePoint = hit.point;
            joint = pl.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distFromPt = Vector3.Distance(pl.position, grapplePoint);

            joint.maxDistance = distFromPt * 0.8f;
            joint.minDistance = distFromPt * 0.25f;

            // may need to alter for best fit
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, hookTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
