using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormStat : MonoBehaviour
{
    // Start is called before the first frame update,
    private float hp;
    public int initValue = 100;
    public HealthBar healthBar;
    public bool isActive = true;

    public bool isDmg = false;
    private int tempDmg = 0;

    void Start() {
        hp = initValue;
        healthBar.setMaxHealth(initValue);
    }

    public void TakeDamage(int dmg) {
        tempDmg = dmg;
        isDmg = true;
        // hp -= dmg;
        //  healthBar.TakeDamage(dmg);
        if (isActive) {
            Camera.main.GetComponentInParent<CameraPlayer>().UnLoadPlayer();
        }
    }

    public void updateDamage() {

    }

}
