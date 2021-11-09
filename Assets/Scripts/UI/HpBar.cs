using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Entity entity;

    Slider _slider;
    
    void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        entity.HpChangedEvent += SetSliderValues;
    }

    void OnDisable()
    {
        entity.HpChangedEvent -= SetSliderValues;
    }

    void SetSliderValues(int current, int maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = current;
    }
}
