using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {

	// Use this for initialization
	public float speedcamera =120.0f;
	public GameObject camerafollowobject;
	public float clampeagle=80.0f;
	public float inputSen = 150f;
	private float mouseX;
	private float mouseY;
	public float fininputX;
	public float fininputZ;
	private float rotX=0f;
	private float rotY=0f;
	
	void Start () 
	{
	Vector3 rot = transform.localRotation.eulerAngles;
	rotY = rot.y;
	rotX = rot.x;
	Cursor.lockState = CursorLockMode.Locked;
	Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mouseX = Input.GetAxis("Mouse X");
		mouseY = Input.GetAxis("Mouse Y");
		//na pada add
		fininputX =  mouseX;
		fininputZ = mouseY;
		rotY += fininputX * inputSen * Time.deltaTime;
		rotX += fininputZ * inputSen * Time.deltaTime;
		rotX = Mathf.Clamp(rotX,-clampeagle,clampeagle);
		
		Quaternion localrotation = Quaternion.Euler(rotX,rotY,0f);
		transform.rotation = localrotation;
		
	}
	void LateUpdate()
	{
		camerafollow();
	}
	void camerafollow()
	{
		Transform cel = camerafollowobject.transform;
		float step = speedcamera * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position,cel.position,step);
	}
}
