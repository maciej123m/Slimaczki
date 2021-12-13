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
    void Start() {
        force = 0f;
        ad = GetComponent<AudioSource>();
        ad.clip = equip;
        ad.Play();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            licznik = true;

        }

        if (licznik) {
            force += Time.deltaTime * 20;
        }

        if (Input.GetMouseButtonUp(0)) {
            ad.clip = fire;
            ad.Play();
            var obj = Instantiate(pocisk, transform.GetChild(0).transform.position, Quaternion.identity);

            obj.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
            force = 0;

        }

    }

}
