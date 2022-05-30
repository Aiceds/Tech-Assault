using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSlider : MonoBehaviour
{
    public Slider slider;

    public void SetCharge(int charge)
    {
        slider.value = charge;
    }
}