using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class moveOLD : MonoBehaviour
{

    //szybkość obrotu kostki
    private float rotationSpeed = 0.1f;

    private CharacterController characterController;

    //szybkość chodzenia
    public float movmentSpeed = 1f;

    //siła grawitacji
    public float gravity = 0.00005f;

    //siła przyciągania
    public float velocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private float inputX;
    private float inputZ;
    private Vector3 foward;
    private Vector3 right;


    //czy może się ruszać
    private bool isMove = false;

    void Update()
    {
        if (isMove) {
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

        }

        //grawitacja
        if (characterController.isGrounded)
        {
            velocity = 0;
        }
        else if (velocity > -0.08) {
            velocity -= gravity * (float) 0.001 * Time.deltaTime;
        }


        characterController.Move(new Vector3(0, velocity, 0));

        UpdateRotationObject();

    }

    private float temp = 0;
    void UpdateRotationObject()
    {
        var speed = new Vector2(inputX, inputZ).sqrMagnitude;
        if (temp - speed <= 0 && speed !=0) {
            var moveDirection = foward * inputZ + right * inputX;
            transform.rotation =
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        }

        temp = speed;
    }

    public void IsMove(bool isMove) {
        this.isMove = isMove;
    }
    //


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
