using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : Bar
{
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _player.CurrentMana;
        _slider.value = _slider.maxValue;
    }

    private void OnEnable()
    {
        _player.ManaChanged += OnStartChangeValue;
    }

    private void OnDisable()
    {
        _player.ManaChanged -= OnStartChangeValue;
    }
}
