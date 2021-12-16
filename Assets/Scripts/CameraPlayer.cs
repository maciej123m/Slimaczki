using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour {

    //wybrana broń
    public GameObject gun;

    //GameManger!
    public GameObject gameManger;

    // Use this for initialization
    public GameObject camerafollowobject;
    public GameObject celownik;

    //skrypt move gracza
    public MoveRidigbody player;

    //kąty maksymalne
    private float minClampEagle = 80.0f;
    private float maxClampEagle = 60f;

    //szybkość ruchu myszy
    public float freeCameraSensitivity = 150f;

    public float aimingCameraSensitivity = 200f;

    private float mouseX;
    private float mouseY;

    private float rotX = 0f;
    private float rotY = 0f;

    //zmiana szybkości "jechania" kamery
    public float speedChangeCamera = 3f;

    //domyślna rotacja
    private Quaternion defaultRotation;


    public bool endAiminng = true;
    //zmienna odpowiadająca za to czy przycisk jest wciśnięty
    private bool keyDown = false;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        defaultRotation = Camera.main.transform.localRotation;
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            keyDown = !keyDown;
            rotX = celownik.transform.parent.eulerAngles.x;
            rotY = celownik.transform.parent.eulerAngles.y;
            endAiminng = false;
            //ustawianie pozycji kamery na środek

            //przy zmianie kamery broń jest wyjmowana lub chowana
            
            //NISZCZENIE OBIEKTU BRONI JEŻELI JEST!
            if (gun != null) {
                Destroy(gun);
            }
            
        }


        if (keyDown)
        {
            //Debug.Log("przycisk wciśnięty");
            Camera.main.GetComponent<CollisionCamera>().enabled = false;
            player.IsMove(false);
            //Jeżeli kamera się ustawi do pozycji ma się odblokować możliwość ruszania kamerą
            if (Vector3.Distance(Camera.main.transform.position, celownik.transform.position) < 0.1f)
            {
                endAiminng = true;
                AimingCamera();
                if (gun == null) {
                    loadGun();
                }
               

            }
            //do momentu aż nie zostanie ustanowiona pozycja
            else if (!endAiminng)
            {
                //obrót rotacji
                Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation,
                    celownik.transform.parent.rotation, 0.05f);

                //zmiana pozycji
                Camera.main.transform.position =
                    Vector3.Lerp(Camera.main.transform.position, celownik.transform.position,
                        Time.deltaTime * speedChangeCamera);
            }
        }
        else
        {
            //Debug.Log("przycisk nie wciśnięty");
            //przywracanie rotacji domyślnej
            Camera.main.transform.localRotation = defaultRotation;
            Camera.main.GetComponent<CollisionCamera>().enabled = true;
            player.IsMove(true);
            FreeCamera();
        }
    }


    public void updateGun() {
        if (gun != null) {
            Destroy(gun);
        }

        gunManger g = gameManger.GetComponent<gunManger>();
        gun = Instantiate(g.guns[g.selectedGun], Camera.main.transform);
    }

    private void loadGun() {
        gunManger g = gameManger.GetComponent<gunManger>();
        gun = Instantiate(g.guns[g.selectedGun], Camera.main.transform);
    }



    void LateUpdate()
    {
        if (!keyDown)
        {
            //Debug.Log("lateUpdate");
            camerafollow();
        }
    }

    private void AimingCamera()
    {

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log(rotY);
        rotY += mouseX * aimingCameraSensitivity * Time.deltaTime;
        rotX -= mouseY * aimingCameraSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -50, 50);
        rotY = Mathf.Clamp(rotY, celownik.transform.parent.eulerAngles.y - 40, celownik.transform.parent.eulerAngles.y + 40);
        Camera.main.transform.eulerAngles = new Vector3(rotX, rotY, 0f);

    }

    private void init() {
        player.IsMove(true);
    }

    private void FreeCamera()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotY += mouseX * freeCameraSensitivity * Time.deltaTime;
        rotX -= mouseY * freeCameraSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -minClampEagle, maxClampEagle);

        Quaternion localrotation = Quaternion.Euler(rotX, rotY, 0f);
        transform.rotation = localrotation;
    }

    void camerafollow()
    {
        Transform cel = camerafollowobject.transform;
        float step = 120f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, cel.position, step);
    }
}
