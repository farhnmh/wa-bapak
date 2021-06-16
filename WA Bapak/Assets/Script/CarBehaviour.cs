using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{

    public Rigidbody sphereRb;

    public float forwardAccel;
    public float reverseAccel;
    public float maxSpeedf;
    public float turnStrength;
    public float gravityForce;

    private float speedInput, turnInput;

    [SerializeField] private bool grounded;
    public LayerMask whatisGround;
    public float groundRayLength;
    public Transform groundRayPoint;

    public float dragOnGround;

    private void Start()
    {
        sphereRb.transform.parent = null;
    }

    private void Update()
    {
        speedInput = 0;
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }

        turnInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
             new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }
       

        transform.position = sphereRb.transform.position;
    }

    private void FixedUpdate()
    {

        grounded = false;

        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatisGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded)
        {

            sphereRb.drag = dragOnGround;

            if (Mathf.Abs(speedInput) > 0)
            {
                sphereRb.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            sphereRb.drag = 0.1f;
            sphereRb.AddForce(Vector3.up * -gravityForce * 100f) ;
        }
       
       
    }
}

