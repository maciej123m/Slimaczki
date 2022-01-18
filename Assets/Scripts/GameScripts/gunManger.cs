using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class gunManger : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> guns;
    public int selectedGun = 0;
    public GameObject canvas;

    void Update() {
        List<KeyCode> l = new List<KeyCode>() {
            KeyCode.Alpha1,
            KeyCode.Alpha2
        };
        for (int i = 0; i < 2; i++) {
            if (Input.GetKeyDown(l[i])) {
                selectedGun = i;
                Camera.main.GetComponentInParent<CameraPlayer>().updateGun();
                switch (i) {
                    case 0:
                        canvas.GetComponent<InterfaceConstroller>().reload(true);
                        break;
                    case 1:
                        canvas.GetComponent<InterfaceConstroller>().reload(false);
                        break;
                }
                
            }
        }

        
    }
}
