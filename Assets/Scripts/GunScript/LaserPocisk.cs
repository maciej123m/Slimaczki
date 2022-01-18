using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPocisk : MonoBehaviour
{
    //eksplozja pociku
    public GameObject rocketExplosion;
    //dŸwiêk podczas lotu
    public AudioSource inFlightAudioSource;

    public ParticleSystem disableOnHit;

    //przenoszony z bazzokascript playerController
    [HideInInspector] public PlayerController playerController;
    //czy eksplodowa³o
    private bool explode = false;

    private Rigidbody rb;
    //maksymalne dmg mo¿liwe do zadania przez ten pocisk
    public int maxDMG = 25;

    //w jakiej odleg³oœci jest odczuwalne
    [Range(1, 20)]
    public float radius = 5f;

    //moc
    [Range(300, 1000)]
    public float power = 500f;

    private double timeToDestroy = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy<0) {
            Destroy(transform.gameObject);

            //danie sygna³u ¿e pocisk ju¿ uderzy³
            playerController.UnLoadWorm();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var collisionPos = transform.position;
        if (explode)
        {
            return;
        }
        StartCoroutine(Explode());
        //disableOnHit.Stop();
        explode = true;
        rb.drag = 1000f;

    }


    IEnumerator Explode()
    {


        // GameObject newExplosion =
        //     Instantiate(rocketExplosion, transform.position, rocketExplosion.transform.rotation, null);

        Collider[] collisions = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in collisions)
        {
            Rigidbody rbHit = hit.GetComponent<Rigidbody>();
            if (rbHit != null)
            {
                var explodePosition = transform.position + Vector3.down * 2;
                if (hit.tag == "Player")
                {
                    int distance = (int)Vector3.Distance(transform.position, hit.transform.position);
                    int dmg = maxDMG / (distance == 0 ? 1 : (distance / 2) == 0 ? 1 : distance / 2);
                    Debug.Log($"DMG: {dmg} distance: {distance}");
                    hit.GetComponent<WormStat>().TakeDamage(dmg);
                }

                rbHit.AddExplosionForce(power, explodePosition, radius);

            }
        }
        Destroy(transform.gameObject);

        //danie sygna³u ¿e pocisk ju¿ uderzy³
        playerController.UnLoadWorm();
        yield return explode;
    }
}
