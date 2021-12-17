using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> playerTeam1;
    public List<GameObject> playerTeam2;

    public GameObject selectedPlayer;

    public CameraPlayer CameraBase;

    public enum NextTeam {
        Team1 = 1,
        Team2 = 2
    }

    private NextTeam nextTeam = NextTeam.Team1;

    private int numberWormsInTeam;

    private int numberWormsTeam1 = 0;

    private int numberWormsTeam2 = 0;

    void Start()
    {
        numberWormsInTeam = playerTeam1.Count;
        LoadWorm();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadWorm() {

        //�adowanie nast�pnych robak�w graczy
        switch (nextTeam) {
            case NextTeam.Team1:
                if (selectedPlayer != null) {
                    selectedPlayer.GetComponent<WormStat>().isActive = false;
                }
                selectedPlayer = playerTeam1[numberWormsTeam1];
                selectedPlayer.GetComponent<WormStat>().isActive = true;
                CameraBase.LoadPlayer(selectedPlayer);
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
                CameraBase.LoadPlayer(selectedPlayer);
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
        CameraBase.LockCamera();
        yield return new WaitForSeconds(3);
        CameraBase.UnLoadPlayer();
        RefreshStatistic();
    }

    private void RefreshStatistic() {
        LoadWorm();
    }
}
