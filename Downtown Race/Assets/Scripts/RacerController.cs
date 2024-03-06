using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerController : MonoBehaviour
{
    public float accelerationForce = 15f;
    public float brakingForce = 5f;
    public float maxSpeed = 15f;
    public float reverseSpeed = 10f;
    public float steeringTorque = 40f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float steeringInput = Input.GetAxis("Horizontal");
        float moveDirection = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;

        rb.AddTorque(transform.up * steeringInput * moveDirection * steeringTorque, ForceMode.Acceleration);

        if (moveDirection != 0f)
        {
            rb.AddForce(transform.forward * moveDirection * (moveDirection > 0 ? accelerationForce : reverseSpeed), ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(-rb.velocity.normalized * brakingForce, ForceMode.Acceleration);
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}