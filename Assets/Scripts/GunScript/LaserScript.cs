using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pocisk;
    private bool licznik = false;

    private AudioSource ad;
    public AudioClip equip;
    public AudioClip fire;

    public bool isShot = false;

    private PlayerController playerController;

    private TimeController timeController;
    private GameObject can;


    void Start()
    {
        // ad = GetComponent<AudioSource>();
        // ad.clip = equip;
        // ad.Play();
        //³adowanie gamecontroller DO ZMIANY!
        playerController = Camera.main.transform.parent.GetComponent<CameraPlayer>().gameManger.GetComponent<PlayerController>();
        timeController = Camera.main.transform.parent.GetComponent<CameraPlayer>().gameManger
            .GetComponent<TimeController>();
        can = GameObject.Find("Aiming");
    }

    // Update is called once per frame
    void Update()
    {
        //je¿eli gracz ju¿ wystrzeli³ pocisk
        if (isShot)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }

    }

    void Shot()
    {
        isShot = true;
        // ad.clip = fire;
        // ad.Play();
        var q = new Quaternion(transform.localRotation.x, transform.localRotation.y, transform.localRotation.z,
            transform.localRotation
                .w);
        var obj = Instantiate(pocisk, transform.GetChild(0).transform.position, Camera.main.transform.rotation);
        Destroy(transform.GetChild(0).gameObject);
        obj.GetComponent<LaserPocisk>().playerController = playerController;
        obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 50f, ForceMode.Impulse);
    }
}
