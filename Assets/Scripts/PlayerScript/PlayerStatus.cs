using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    private Vector3 oldPosition = Vector3.zero;

    public bool isMoving;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Round(rb.velocity,0) != Vector3.zero) {
            isMoving = true;
        }
        else {
            isMoving = false;
        }
        // if (oldPosition != Round(transform.position.normalized,3)) {
        //     isMoving = true;
        // }
        // else {
        //     isMoving = false;
        // }
        // oldPosition = Round(transform.position.normalized,3);

    }

    public static Vector3 Round(Vector3 vector3, int decimalPlaces = 2) {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++) {
            multiplier *= 10f;
        }

        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }
}

