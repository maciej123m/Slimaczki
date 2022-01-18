using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormStat : MonoBehaviour
{
    // Start is called before the first frame update,
    private float hp;
    public int initValue = 100;
    public HealthBar healthBar;

    public bool isActive = false;

    public bool isDmg = false;
    private int tempDmg = 0;

    void Start() {
        hp = initValue;
        healthBar.setMaxHealth(initValue);
    }

    public void TakeDamage(int dmg) {
        tempDmg = dmg;
        isDmg = true;
      
        if (isActive) {
            Camera.main.GetComponentInParent<CameraPlayer>().UnLoadPlayerFromCameraObject();
        }
    }

    public void Activate() {
        isActive = true;
        transform.Find("Canvas").GetComponent<Canvas>().enabled = false;
    }

    public void Deactivate() {
        isActive = false;
        transform.Find("Canvas").GetComponent<Canvas>().enabled = true;

    }
    /// <summary>
    /// odpowiada za przeliczenie �ycia
    /// </summary>
    /// <returns>zwraca czy gracz w�a�nie b�dzie gin�� czy nie</returns>
    public bool UpdateHealth() {
         hp -= tempDmg;
         healthBar.TakeDamage(tempDmg);
         tempDmg = 0;
         isDmg = false;
         if (hp <= 0) {
             return true;
         }
         return false;
    }

    public int GetTempDmg() {
        return tempDmg;
    }

}
