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
    private bool enabled = false;

    private MeshRenderer mesh;

    private Rigidbody rb;

    public SphereCollider col;

    // Start is called before the first frame update
    void Start() {
        mesh = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (explode) {
            return;
        }
        mesh.enabled = false;
        Explode();
        disableOnHit.Stop();
        explode = true;
        rb.drag = 1000f;
        col.enabled = true;

    }

    private void OnCollisionStay(Collision collision) {
        if (!explode) {
            return;
        }
        else {
            if (enabled) {
                return;
            }
            Debug.Log(collision.transform.name);

            enabled = true;
        }
    }

    private void Explode() {
        // --- Instantiate new explosion option. I would recommend using an object pool ---
        GameObject newExplosion =
            Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);
    }
}
