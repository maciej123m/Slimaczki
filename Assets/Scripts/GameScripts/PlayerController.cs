using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> playerTeam1;
    public List<GameObject> playerTeam2;

    public GameObject selectedPlayer;
    public GameObject cameraBase;

    private CameraPlayer cameraPlayer;
    private PassThroughCamera passThroughCamera;

    public TimeController timeController;

    public float timeDelay = 2f;
    public enum NextTeam {
        Team1 = 1,
        Team2 = 2
    }

    private NextTeam nextTeam = NextTeam.Team1;

    private int numberWormsInTeam;

    private int numberWormsTeam1 = 0;

    private int numberWormsTeam2 = 0;

    //canvas z oczekiwaniem na klikniêcie spacji
    public Canvas waitCanvas;

    void Start() {
        cameraPlayer = cameraBase.GetComponent<CameraPlayer>();
        passThroughCamera = cameraBase.GetComponent<PassThroughCamera>();
        numberWormsInTeam = playerTeam1.Count;
        LoadWorm();
    }


    private void LoadWorm() {

        switch (nextTeam) {
            case NextTeam.Team1:
                selectedPlayer = playerTeam1[numberWormsTeam1];
                selectedPlayer.GetComponent<WormStat>().Activate();
                nextTeam = NextTeam.Team2;
                if (numberWormsTeam1+1 == numberWormsInTeam) {
                    numberWormsTeam1 = 0;
                }
                else {
                    numberWormsTeam1++;
                }
                break;
            case NextTeam.Team2:
                selectedPlayer = playerTeam2[numberWormsTeam2];
                selectedPlayer.GetComponent<WormStat>().Activate();
                nextTeam = NextTeam.Team1;
                if (numberWormsTeam2 + 1 == numberWormsInTeam) {
                    numberWormsTeam2 = 0;
                }
                else {
                    numberWormsTeam2++;
                }
                break;
        }
        StartCoroutine(MoveCameraToStartPlayer(selectedPlayer));

    }

    IEnumerator MoveCameraToStartPlayer(GameObject player) {
        passThroughCamera.JumpToPlayer(player, PassThroughCamera.Tryb.Przygotowanie);
        yield return new WaitWhile(() => Vector3.Distance(Camera.main.transform.parent.transform.position,
            player.transform.Find("CameraFollowObject").transform.position) > 3.3);
        //kliknij spacje
        waitCanvas.gameObject.SetActive(true);
        yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.Space));
        waitCanvas.gameObject.SetActive(false);
        //³adowanie gracza do kamery
        cameraPlayer.LoadPlayer(selectedPlayer);
        //tutaj wyœwietl ekran
        timeController.reloadAndStartTime();
        passThroughCamera.enabled = false;
        cameraPlayer.enabled = true;
    }
    /// <summary>
    /// ta funkcja jest wywo³ana po wystrzale broni przez skrypt broni
    /// </summary>
    public void UnLoadWorm() {
        //³adowanie nastêpnych robaków graczy
        if (selectedPlayer != null)
        {
            selectedPlayer.GetComponent<WormStat>().Deactivate();
        }
        timeController.stopTime();
        StartCoroutine(UnLoadWormCoroutine());
    }

    /// <summary>
    /// czas oczekiwania dezaktywacje gracza
    /// </summary>
    /// <returns>null</returns>
    IEnumerator UnLoadWormCoroutine() {
        cameraPlayer.LockCamera();
        yield return new WaitForSeconds(3);
        //oczekiwanie a¿ wszyscy gracze przesatn¹ siê ruszaæ
        List<PlayerStatus> completeList = new List<PlayerStatus>();
        completeList.AddRange(playerTeam1.Select(x => x.GetComponent<PlayerStatus>()));
        completeList.AddRange(playerTeam2.Select(x => x.GetComponent<PlayerStatus>()));
        bool isMoving = false;
        do {
            isMoving = false;
            foreach (var player in completeList) {
                if (player.isMoving) {
                    isMoving = true;
                    yield return new WaitForSeconds(2);
                    break;
                }
            }
        } while (isMoving);
        //jak przestan¹ siê ruszaæ kod idzie dalej
        cameraPlayer.UnLoadPlayerFromCameraObject();
        //zmiana systemów kamery
        cameraPlayer.enabled = false;
        passThroughCamera.enabled = true;
        //LoadWorm();
        RefreshStatistic();
    }

    // void temp() {
    //     List<PlayerStatus> completeList = new List<PlayerStatus>();
    //     completeList.AddRange(playerTeam1.Select(x => x.GetComponent<PlayerStatus>()));
    //     completeList.AddRange(playerTeam2.Select(x => x.GetComponent<PlayerStatus>()));
    //     bool isMoving = false;
    //     do {
    //         isMoving = false;
    //         foreach (var player in completeList) {
    //             if (player.isMoving) {
    //                 isMoving = true;
    //                 yield return new WaitForSeconds(1);
    //                 break;
    //             }
    //         }
    //     } while (isMoving);
    // }

    private void RefreshStatistic() {
        List<GameObject> completeList = new List<GameObject>();
        completeList.AddRange(playerTeam1.Where(x => x.GetComponent<WormStat>().isDmg == true).ToList());
        completeList.AddRange(playerTeam2.Where(x => x.GetComponent<WormStat>().isDmg == true).ToList());
        if (completeList.Count == 0) {
            LoadWorm();
            return;
        }
        var tempList = new List<GameObject>();

        tempList.Add(completeList[0]);
        completeList.RemoveAt(0);

        //je¿eli by³ tylko 1 gracz który ucierpia³ nie rób reszty
        if (completeList.Count == 0) {
            StartCoroutine(MoveCameraToPlayer(tempList, 0));
            return;
        }

        int tempPlayer = 0;
        float tempDistance = 999;
        //wyszukiwanie gracza najbli¿ej kamery
        for (int i = 0; i < completeList.Count; i++) {
            var distance = Vector3.Distance(Camera.main.transform.position,
                completeList[i].transform.position);

            if (distance < tempDistance) {
                tempPlayer = i;
                tempDistance = distance;
            }
        }
        
        tempList.Add(completeList[tempPlayer]);
        completeList.RemoveAt(tempPlayer);
        //koniec

        while (completeList.Count != 0) {
            tempPlayer = 0;
            tempDistance = 999;
            for (int i = 0; i < completeList.Count; i++) {
                var distance = Vector3.Distance(tempList[tempList.Count - 1].transform.position,
                    completeList[i].transform.position);

                if (distance < tempDistance) {
                    tempPlayer = i;
                    tempDistance = distance;
                }
            }
            tempList.Add(completeList[tempPlayer]);
            completeList.RemoveAt(tempPlayer);
        }
        Debug.Log(tempList.Count);
        StartCoroutine(MoveCameraToPlayer(tempList,0));
    }

    //REKURENCYJNE
    IEnumerator MoveCameraToPlayer(List<GameObject> players, int i) {
        passThroughCamera.JumpToPlayer(players[i], PassThroughCamera.Tryb.Statystyki);
        yield return new WaitWhile(() => Vector3.Distance(transform.position,players[i].transform.position) == 0);
        yield return new WaitForSeconds(2);
        var playerWormStat = players[i].GetComponent<WormStat>();
        int dmg = playerWormStat.GetTempDmg();
        bool isDead = playerWormStat.UpdateHealth();
        float czas = timeDelay / dmg;
        yield return new WaitForSeconds(czas+3);
        if (isDead) {
            //animacja zginiêcia + dezaktywacja
        }
        if (i+1 < players.Count) {
            StartCoroutine(MoveCameraToPlayer(players, i + 1));
        }
        else {
            LoadWorm();
            yield return i;
        }
    }

    void ShowRayBetweenPoints(Vector3 p1, Vector3 p2) {
        Debug.DrawRay(p1, (p2 - p1), Color.yellow);
    }
}
