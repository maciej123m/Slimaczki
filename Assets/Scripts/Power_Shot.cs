using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power_Shot : MonoBehaviour
{

    public int CurrentPower;
    public int MaxPower = 60;
    public Slider slider;
    public Gradient gradient;
    public Image Fill;
    public int Damage;

   
    // Start is called before the first frame update
    void Start()
    {
        CurrentPower = MaxPower;
        slider.maxValue = MaxPower;
        slider.value = CurrentPower;

        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetForce(int force)
    {
        slider.value = force;
    }
    public void set_power()
    {
        slider.value = slider.normalizedValue;
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
