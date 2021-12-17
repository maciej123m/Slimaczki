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

    public float timeDelay = 2f;
    public enum NextTeam {
        Team1 = 1,
        Team2 = 2
    }

    private NextTeam nextTeam = NextTeam.Team1;

    private int numberWormsInTeam;

    private int numberWormsTeam1 = 0;

    private int numberWormsTeam2 = 0;

    void Start() {
        cameraPlayer = cameraBase.GetComponent<CameraPlayer>();
        passThroughCamera = cameraBase.GetComponent<PassThroughCamera>();
        numberWormsInTeam = playerTeam1.Count;
        LoadWorm();
    }


    private void LoadWorm() {
        passThroughCamera.enabled = false;
        cameraPlayer.enabled = true;
        //³adowanie nastêpnych robaków graczy
        switch (nextTeam) {
            case NextTeam.Team1:
                if (selectedPlayer != null) {
                    selectedPlayer.GetComponent<WormStat>().isActive = false;
                }
                selectedPlayer = playerTeam1[numberWormsTeam1];
                selectedPlayer.GetComponent<WormStat>().isActive = true;
                cameraPlayer.LoadPlayer(selectedPlayer);
                nextTeam = NextTeam.Team2;
                if (numberWormsTeam1+1 == numberWormsInTeam) {
                    numberWormsTeam1 = 0;
                }
                else {
                    numberWormsTeam1++;
                }
                break;
            case NextTeam.Team2:
                if (selectedPlayer != null) {
                    selectedPlayer.GetComponent<WormStat>().isActive = false;
                }
                selectedPlayer = playerTeam2[numberWormsTeam2];
                cameraPlayer.LoadPlayer(selectedPlayer);
                selectedPlayer.GetComponent<WormStat>().isActive = true;
                nextTeam = NextTeam.Team1;
                if (numberWormsTeam2 + 1 == numberWormsInTeam) {
                    numberWormsTeam2 = 0;
                }
                else {
                    numberWormsTeam2++;
                }
                break;
        }

    }

    public void UnLoadWorm() {
        StartCoroutine(delay());
    }

    //UNIWERSALNY DELAY
    IEnumerator delay() {
        cameraPlayer.LockCamera();
        yield return new WaitForSeconds(3);
        cameraPlayer.UnLoadPlayer();
        cameraPlayer.enabled = false;
        //LoadWorm();
        RefreshStatistic();
    }

    private void RefreshStatistic() {
        List<GameObject> completeList = new List<GameObject>();
        completeList.AddRange(playerTeam1.Where(x => x.GetComponent<WormStat>().isDmg == true).ToList());
        completeList.AddRange(playerTeam2.Where(x => x.GetComponent<WormStat>().isDmg == true).ToList());
        if (completeList.Count == 0) {
            LoadWorm();
            return;
        }
        var tempList = new List<GameObject>();
        int licznik = 0;

        tempList.Add(completeList[0]);
        completeList.RemoveAt(0);
        int tempPlayer = 0;
        float tempDistance = 999;
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

        passThroughCamera.enabled = true;
        StartCoroutine(MoveCameraToPlayer(tempList,0));
    }

    IEnumerator MoveCameraToPlayer(List<GameObject> players, int i) {
        passThroughCamera.JumpToPlayer(players[i]);
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
        //SUWANIE KAMERY I ODEJMOWANIE DMG ROBAKOM!
        //REKURENCYJNE
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
