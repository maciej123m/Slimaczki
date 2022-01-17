using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    public double maxTime = 10;
    public double availableTime = 0;
    public bool start = false;
    public PlayerController controller;

    void FixedUpdate() {
        if (start) {
            availableTime -= Time.deltaTime;
            Debug.Log(availableTime);
            if (availableTime < 0) {
                start = false;
                controller.UnLoadWorm();
            }
        }
    }

    public void reloadAndStartTime() {
        availableTime = maxTime;
        start = true;
    }

    public void stopTime() {
        start = false;
    }
}
