using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : Bar
{
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.CurrentHealth;
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnStartChangeValue;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnStartChangeValue;
    }
}
