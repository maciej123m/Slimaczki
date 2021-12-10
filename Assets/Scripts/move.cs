using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class move : MonoBehaviour
{

    // Use this for initialization

    private float rotationSpeed = 0.1f;

    private CharacterController characterController;
    public float movmentSpeed = 1f;

    public float normalMovementSpeed = 3f;
    public float runMovementSpeed = 8f;
    public float gravity = 0.00005f;
    public float velocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private float inputX;
    private float inputZ;
    private Vector3 foward;
    private Vector3 right;



    void Update()
    {
        //ruch postaci wedle ułożenia kamery
        inputX = Input.GetAxis("Horizontal") * movmentSpeed;
        inputZ = Input.GetAxis("Vertical") * movmentSpeed;

        foward = Camera.main.transform.forward;
        right = Camera.main.transform.right;

        foward.y = 0f;
        right.y = 0f;

        foward.Normalize();
        right.Normalize();

        Vector3 MoveDirection = foward * inputZ + right * inputX;

        characterController.Move(MoveDirection * Time.deltaTime);



        //grawitacja
        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else if (velocity > -0.08) {
            velocity -= gravity * (float) 0.0001;
        }


        characterController.Move(new Vector3(0, velocity, 0));

        UpdateRotationObject();

    }

    void UpdateRotationObject()
    {
        var speed = new Vector2(inputX, inputZ).sqrMagnitude;
        //Debug.Log(speed);
        if (speed > 0) {
            var moveDirection = foward * inputZ + right * inputX;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
           
        }
    }



    /*    
     *    
     *    
     *    void setmove()
    {
        inputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        speed = new Vector2(inputX,InputZ).sqrMagnitude;
        if(speed > allowPlayerRotation)
        {
            UpdatePositionAndRotationPlayer();
        }

    }
     *    
     *    mouseY =Input.GetAxis("Mouse Y");
        d
    
        if(con.isGrounded)
        {
            vector = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            vector = transform.TransformDirection(vector);
            vector *= speed;        
            
        }
        vector.y -= 20f*Time.deltaTime;
        if(Input.GetKey(KeyCode.LeftShift))
        {    
            con.Move(vector*2 * Time.deltaTime);
        }    
        else
        {
            
            con.Move(vector * Time.deltaTime);
        }*/








}
