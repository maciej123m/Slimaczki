using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sloop : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform rayOrigin;
    void Start()
    {
        
    }

    private RaycastHit slopeHit;
    // Update is called once per frame
    private Vector3 slopeMoveDirection;
    void FixedUpdate() {
        onSlope();
       

    }

    void OnDrawGizmosSelected() {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(slopeMoveDirection, 1);
    }

    private void onSlope() {
        if (Physics.Raycast(rayOrigin.position, Vector3.down, out slopeHit, transform.lossyScale.y / 2 + 0.5f)) {
            transform.rotation = new Quaternion(Quaternion.FromToRotation(Vector3.up, slopeHit.normal).x,transform.rotation.y,
                transform.rotation.z,transform.rotation.w);
        }
    }

}
