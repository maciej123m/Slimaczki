using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public int currentHealth;

    private Slider slider;
    public float time = 2f;

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
            currentHealth -= 1;
            slider.value = currentHealth;
            yield return new WaitForSeconds(czas);
        }
    }

}
