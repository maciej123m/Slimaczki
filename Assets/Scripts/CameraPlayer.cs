using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {

	// Use this for initialization
	public float speedcamera =120.0f;
	public GameObject camerafollowobject;
    public GameObject celownik;

	//kąty maksymalne
	private float minClampEagle=80.0f;
    private float maxClampEagle = 60f;

	public float inputSen = 150f;
	private float mouseX;
	private float mouseY;

	private float rotX=0f;
	private float rotY=0f;

	//zmiana szybkości "jechania" kamery
    public float speedChangeCamera = 3f;

    private Quaternion defaultRotation;

	//zmienna odpowiadająca za to czy przycisk jest wciśnięty
	private bool keyDown = false;
	void Start () 
	{
	    Vector3 rot = transform.localRotation.eulerAngles;
	    rotY = rot.y;
	    rotX = rot.x;
	    Cursor.lockState = CursorLockMode.Locked;
	    Cursor.visible = false;
        defaultRotation = Camera.main.transform.localRotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
			keyDown = !keyDown;
        }

		if (keyDown) {
			//Debug.Log("przycisk wciśnięty");
            Camera.main.GetComponent<CollisionCamera>().enabled = false;

            Camera.main.GetComponent<MouseHandler>().enabled = true;
			//ustawianie pozycji kamery na środek

			Camera.main.transform.position =
                Vector3.Lerp(Camera.main.transform.position, celownik.transform.position,
                    Time.deltaTime * speedChangeCamera);

        }
		else {
            //Debug.Log("przycisk nie wciśnięty");
            Camera.main.GetComponent<MouseHandler>().enabled = false;
            //przywracanie rotacji domyślnej
            Camera.main.transform.localRotation = defaultRotation;
			Camera.main.GetComponent<CollisionCamera>().enabled = true;
			FreeCamera();
        }
	}

 


    void LateUpdate()
	{
        if (!keyDown) {
			//Debug.Log("lateUpdate");
            camerafollow();
        }
    }

    private void FreeCamera() {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * inputSen * Time.deltaTime;
        rotX += mouseY * inputSen * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -minClampEagle, maxClampEagle);

        Quaternion localrotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localrotation;
	}

	void camerafollow()
	{
		Transform cel = camerafollowobject.transform;
		float step = speedcamera * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position,cel.position,step);
	}
}
