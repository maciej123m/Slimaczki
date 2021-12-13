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

    private MeshRenderer mesh;

    private Rigidbody rb;

    public SphereCollider col;

    [Range(1,20)]
    public float radius = 5f;

    [Range(1, 20)]
    public float power = 10f;

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


        Collider[] collisions = Physics.OverlapSphere(transform.position, 5f);

        foreach (var hit in collisions) {
            var rb = hit.GetComponent<Rigidbody>();
            if (this.rb != null) {
                rb.AddExplosionForce(power,transform.position,radius,3f);
            }
        }
    }


    private void Explode() {
        // --- Instantiate new explosion option. I would recommend using an object pool ---
        GameObject newExplosion =
            Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);
    }
}
