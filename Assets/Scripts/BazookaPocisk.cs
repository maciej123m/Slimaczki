using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaPocisk : MonoBehaviour
{
    //eksplozja pociku
    public GameObject rocketExplosion;

    public AudioSource inFlightAudioSource;

    public ParticleSystem disableOnHit;

    private bool explode = false;

    private Rigidbody rb;

    [Range(1,20)]
    public float radius = 5f;

    [Range(300, 1000)]
    public float power = 500f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision) {
        var collisionPos = transform.position;
        if (explode) {
            return;
        }
        StartCoroutine(Explode());
        disableOnHit.Stop();
        explode = true;
        rb.drag = 1000f;

    }


    IEnumerator Explode() {

        GameObject newExplosion =
            Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in collisions)
        {
            Rigidbody rbHit = hit.GetComponent<Rigidbody>();
            if (rbHit != null)
            {
                if(hit.tag == "Player")
                {
                    //TUTAJ B�DZIE FUNKCJA ZADAJ�CA DMG TEMU GAMEOBJECT
                }
                rbHit.AddExplosionForce(power, transform.position, radius);
            }
        }
        Destroy(transform.gameObject);
        yield return explode;
    }
}
