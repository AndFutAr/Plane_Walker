using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallLogic : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) rb.AddForce(Vector3.up * 0.05f * Time.deltaTime, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "BlackBox") rb.AddForce(Vector3.up * 0.1f * Time.deltaTime, ForceMode.Impulse);
        else if (other.collider.tag == "Finish")
        {
            Lever lever = other.collider.gameObject.GetComponent<Lever>();
            if(lever.Active()) rb.AddForce(Vector3.up * 0.1f * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
