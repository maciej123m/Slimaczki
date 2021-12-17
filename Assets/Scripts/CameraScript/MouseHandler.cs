using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour {

    //pr�dko�� obrotu kamery
    public float horizontalSpeed = 1.5f;
    public float verticalSpeed = 1.5f;


    //rotacja kamery
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;
        yRotation -= mouseY;
        xRotation += mouseX;
        //dzi�ki temu gracz nie mo�� odwr�ci� kamery za mocno do g�ry lub za mocno na d�, k�t jest ograniczony
        yRotation = Mathf.Clamp(yRotation, -89, 89);
        //obr�t kamery
        Camera.main.transform.eulerAngles = new Vector3(yRotation,xRotation, 0f);
    }
}
