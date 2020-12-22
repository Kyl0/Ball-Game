using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private float maxDistance = 15f;
    private SpringJoint joint;
    public LayerMask grappleable;
    public Transform hookTip, cam, pl;
    private float hitDistance;  //variable used for debugging the spherecast and visualizing it
    private Vector3 origin;
    public float castHeight = 3f;       //variable that determines the height from the player that the sphereCast will start at

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //origin = pl.position;

        //origin.y += 5f;
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
        Vector3 distance = pl.position;
        distance.z += maxDistance;
        Debug.Log(distance);
        if (Physics.CapsuleCast(pl.position, distance, 10f, cam.forward, out hit, grappleable))
        {
            grapplePoint = hit.point;
            hitDistance = hit.distance;
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

        origin = pl.position;

        origin.y += castHeight;

        if (Physics.SphereCast(origin, 2f, Quaternion.Euler(-10f, 0, 0) * cam.forward, out hit, maxDistance, grappleable))
        {
            grapplePoint = hit.point;
            //hitDistance = hit.distance;
            joint = pl.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distFromPt = Vector3.Distance(origin, grapplePoint);

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
