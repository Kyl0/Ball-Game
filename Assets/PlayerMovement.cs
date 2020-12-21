using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;

    private readonly float force = 1000f;

    private void FixedUpdate()
    {
        //Vector3 camDir = Camera.main.transform.forward;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //Vector3 dir = Camera.main.transform.forward.normalized;
        //dir.y = 0.0f;

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        dir = Camera.main.transform.TransformDirection(dir);  // Make relative to main camera
        dir.y = 0;  // optional for no y movement.
        Vector3 movement = dir.normalized * dir.magnitude;

        rb.AddForce(movement * force * Time.deltaTime);

        /*if ( dir.magnitude >= 0.1f)
        {
            //float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.Euler(0f, targetangle, 0f);


            rb.AddForce(dir * force * Time.deltaTime);
        }*/
    }


    ///TODO: will be using the AddForce function to boost the ball around

}
