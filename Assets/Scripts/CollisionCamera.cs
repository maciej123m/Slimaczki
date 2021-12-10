using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCamera : MonoBehaviour {

	public float minDis = 1.0f;
	public float maxdis = 4.0f;
	public float smooth = 10.0f;
	Vector3 doll;
    public float distance;

    public float scroll;
	void Start () {
		doll = transform.localPosition.normalized;
		distance = transform.localPosition.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		scroll = Input.GetAxis("Mouse ScrollWheel");
		if(maxdis <=9f && maxdis>=0.9f)
		{
			maxdis += scroll;		
		}
		else
		{
			maxdis = 8.9f;
		}
		if(maxdis<1.1f)
		{
			maxdis=1.2f;
		}

		Vector3 camerapos = transform.parent.TransformPoint(doll * maxdis);
		RaycastHit hit;
		if(Physics.Linecast(transform.parent.position,camerapos,out hit))
		{
			distance = Mathf.Clamp(hit.distance*0.9f,minDis,maxdis);
		}
		else
		{
			distance = maxdis;
		}
		transform.localPosition = Vector3.Lerp(transform.localPosition,doll*distance, Time.deltaTime * smooth);
	}
}
