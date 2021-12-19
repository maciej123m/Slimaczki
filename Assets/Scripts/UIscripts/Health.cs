using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int CurrentHealth;
    public int MaxHealth = 100;
    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    public int Damage;


    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        slider.maxValue = MaxHealth;
        slider.value = CurrentHealth;

        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void set_hp ()
        {
        slider.value = slider.normalizedValue;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
