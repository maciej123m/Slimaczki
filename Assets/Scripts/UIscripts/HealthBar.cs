using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public int currentHealth;

    private Slider slider;
    public float time = 2f;
    public Text textDmg;
    private int intDmg;
    void Start() {
        slider = GetComponent<Slider>();
        currentHealth = 100;
    }

    public void setMaxHealth(int hp) {
        if (slider == null) {
            slider = GetComponent<Slider>();
        }
        currentHealth = hp;
        slider.maxValue = hp;
    }
    public void TakeDamage(int damage) {
        StartCoroutine(setHealth(damage));
    }

    IEnumerator setHealth(int dmg) {
        float czas = time / dmg;
        for (int i = 0; i < dmg; i++) {
            if (intDmg - dmg < 0) {
                if (dmg - intDmg < 3) {
                    intDmg += dmg - intDmg;
                }
                else {
                    intDmg += 3;
                }
            }

            textDmg.text = $"-{intDmg}";
            currentHealth -= 1;
            slider.value = currentHealth;
            yield return new WaitForSeconds(czas);
        }

        intDmg = 0;
        textDmg.text = "";
    }

}
