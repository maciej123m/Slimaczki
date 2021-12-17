using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> playerTeam1;
    public List<GameObject> playerTeam2;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
