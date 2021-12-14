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

    [Range(1,20)]
    public float radius = 5f;

    [Range(300, 1000)]
    public float power = 500f;

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
        var collisionPos = transform.position;
        if (explode) {
            return;
        }
        mesh.enabled = false;
        Explode();
        disableOnHit.Stop();
        explode = true;
        rb.drag = 1000f;
    }


    private void Explode() {

        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);
        Debug.Log("bang");
        foreach (var hit in collisions) {
            var rbHit = hit.GetComponent<Rigidbody>();
            if (rbHit != null) {
                rb.AddExplosionForce(500f, transform.position, 25);
            }
        }

        GameObject newExplosion =
            Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

    }
}
