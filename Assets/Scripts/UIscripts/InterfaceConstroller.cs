using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceConstroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bar;


    public void reload(bool barVisibility) {
        bar.gameObject.SetActive(barVisibility);
    }
}
