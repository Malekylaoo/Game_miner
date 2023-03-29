using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : Bar
{
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.CurrentStamina;
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _player.StaminaChanged += OnStartChangeValue;
    }

    private void OnDisable()
    {
        _player.StaminaChanged -= OnStartChangeValue;
    }
}
