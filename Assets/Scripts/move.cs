using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	// Use this for initialization
	
	public GameObject cam;
	private CharacterController con;
	private Transform tran;
	private float rotationspeed;

	void Start () 
	{
		con = GetComponent<CharacterController>();
		tran = GetComponent<Transform>();
	}
	
	float inputX;
	float InputZ;
	private Vector3 MoveDirection;
	public bool rotationPlayer;
	private float speed;
	public float verticallevel;
	private Vector3 movevector;
	private float allowPlayerRotation;
	private float sp;
	public float speedrun = 3f;

	public float sprint=6f;
	
	void Update () 
	{
	
		setmove();
		if(con.isGrounded)
		{
			verticallevel -= 0;
			verticallevel = 0;
		}
		else
		{
			verticallevel -= 6;
		}
		if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
		{
			sp = speedrun;
		}
		else
		{
			sp = 0f;
		}
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.A)&&Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.D)&&Input.GetKey(KeyCode.LeftShift))
		{
			sp = sprint;
		}
		
		movevector = transform.TransformDirection(new Vector3(0f,verticallevel,sp)*Time.deltaTime);
		con.Move(movevector);
		
	
	}

	void UpdatePositionAndRotationPlayer()
	{
		inputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");

		var camera = cam;
		var foward = cam.transform.forward;
		var right = cam.transform.right;

		foward.y=0f;
		right.y=0f;

		foward.Normalize();
		right.Normalize();

		MoveDirection = foward * InputZ + right * inputX;
		if(rotationPlayer == false)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDirection), rotationspeed);
		}
	}

	void setmove()
	{
		inputX = Input.GetAxis("Horizontal");
		InputZ = Input.GetAxis("Vertical");
		speed = new Vector2(inputX,InputZ).sqrMagnitude;
		if(speed > allowPlayerRotation)
		{
			UpdatePositionAndRotationPlayer();
		}

	}

	/*	mouseY =Input.GetAxis("Mouse Y");
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
