using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BazzokaScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pocisk;
    private float force;
    private bool licznik = false;
    private AudioSource ad;
    public Text t;

    public AudioClip equip;
    public AudioClip fire;


    [Range(50,150)]
    public float limit = 60;

    public bool isShot = false;

    private PlayerController gameController;

    void Start() {
        force = 0f;
        ad = GetComponent<AudioSource>();
        ad.clip = equip;
        ad.Play();
        //³adowanie gamecontroller DO ZMIANY!
        gameController = Camera.main.transform.parent.GetComponent<CameraPlayer>().gameManger.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //je¿eli gracz ju¿ wystrzeli³ pocisk
        if (isShot) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            licznik = true;
        }

        if (licznik) {
            if (force < limit) {
                force += Time.deltaTime * 20;
            }
        }

        if (force!=0) {
            Debug.Log(force);
        }

        if (Input.GetMouseButtonUp(0)) {
            isShot = true;
            ad.clip = fire;
            ad.Play();
            var q  = new Quaternion(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z,transform.localRotation
                .w);
            var obj = Instantiate(pocisk, transform.GetChild(0).transform.position, Camera.main.transform.rotation);
            Destroy(transform.GetChild(0).gameObject);
            obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
            force = 0;
            licznik = false;
            gameController.UnLoadWorm();
        }

    }

}
