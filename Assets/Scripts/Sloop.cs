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

    // Update is called once per frame
    void FixedUpdate() {
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity)) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance,
                Color.yellow);
            //Debug.Log(Math.Round(hit.distance, 3));

            //Debug.Log(Vector3.Angle(transform.position, new Vector3(transform.position.x,transform.position.y-hit.distance,transform.position.z)));
            //Debug.Log(Vector3.Angle(transform.position, hit.point));
            Debug.Log(hit.point);
            if (Math.Round(hit.distance,3) >= 1.05f) { 
                Debug.Log("wy¿ej");
            }
            else if (Math.Round(hit.distance, 3) <= 0.95f) {
                Debug.Log("ni¿ej");
            }

            
        }
    }

}
