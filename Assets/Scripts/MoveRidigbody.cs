using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRidigbody : MonoBehaviour
{
    //szybkoœæ obrotu kostki
    public float rotationSpeed = 13f;

    //szybkoœæ chodzenia
    public float movmentSpeed = 5f;

    [Range(3,10)]
    public float jumpForce = 5f;

    [Range(2, 7)]
    public float jumpLenght = 3f;

    private Vector3 moveDirection;

    private float inputX;
    private float inputZ;
    private Vector3 foward;
    private Vector3 right;

    private Rigidbody rb;

    //czy mo¿e siê ruszaæ
    public bool isMove = false;

    //czy jest na ziemi
    public bool isGrounded = true;

    public bool isJump = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        foward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        foward.Normalize();
        right.Normalize();

        foward.y = 0;
        right.y = 0;


        if (isGrounded && !isJump && isMove) {
            updateSpeedObject();
            UpdateRotationObject();
        }

        // float h = Input.GetAxisRaw("Horizontal");
        // float v = Input.GetAxisRaw("Vertical");
        //
        // Vector3 tempVect = new Vector3(h, 0, v);
        // tempVect = tempVect.normalized * movmentSpeed * Time.deltaTime;
        // rb.MovePosition(transform.position + tempVect);


    }

    void updateSpeedObject() {
        //ruch postaci wedle u³o¿enia kamery
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        float input = 0;

        if (inputX != 0 || inputZ != 0) {
            input = 1;
        }
        else {
            input = 0;
        }
        //Debug.Log(input);

        var fowardPlayer = transform.forward;
        var rightPlayer = transform.right;

        fowardPlayer.Normalize();
        rightPlayer.Normalize();

        fowardPlayer.y = 0;
        rightPlayer.y = 0;

        moveDirection = fowardPlayer * Math.Abs(input);// + right * inputX;

        moveDirection.Normalize();

        //SKOK
        if (Input.GetKeyDown(KeyCode.Space)) {
            var m = moveDirection * jumpLenght;
            m.y = jumpForce;
            rb.velocity += m;
            isJump = true;
        }

        Vector3 movementVelocity = moveDirection * movmentSpeed * Time.deltaTime;
        //Debug.Log(movementVelocity);
        rb.MovePosition(transform.position+movementVelocity);
    }

    private float temp = 0;
    void UpdateRotationObject() {
        var speed = new Vector2(inputX, inputZ).sqrMagnitude;
        if (temp - speed <= 0 && speed != 0) {
            var targetDirection = foward * inputZ + right * inputX;
            targetDirection.Normalize();
            targetDirection.y = 0;

            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), rotationSpeed * Time.deltaTime);
        }

        temp = speed;
    }


    void OnCollisionEnter(Collision collision) {
        isGrounded = true;
        isJump = false;
    }

    void OnCollisionStay(Collision collision) {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }
    public void IsMove(bool isMove) {
        this.isMove = isMove;
    }

}
