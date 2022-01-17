using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private RaycastHit slopeHit;
    // Update is called once per frame
    private Vector3 slopeMoveDirection;
    void FixedUpdate() {
        onSlope();
        slopeMoveDirection = Vector3.ProjectOnPlane(transform.position, slopeHit.normal);
        //Debug.Log(slopeMoveDirection);

    }

    void OnDrawGizmosSelected() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(slopeMoveDirection, 1);
    }

    private bool onSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, transform.lossyScale.y / 2 + 0.5f)) {
            if (slopeHit.normal != Vector3.up) {
                return true;
            }
            else {
                return false;
            }
        }

        return true;
    }

}
