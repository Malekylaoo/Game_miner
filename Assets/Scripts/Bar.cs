using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected Player _player;

    protected Coroutine _coroutine;
    protected Slider _slider;

    protected void OnStartChangeValue(float value)
    {
        CheckCoroutine(StartCoroutine(ChangeValue(value)));
    }

    private void CheckCoroutine(Coroutine coroutine)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = coroutine;
    }

    private IEnumerator ChangeValue(float target)
    {
        while(_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, Time.deltaTime * _speed);
            yield return null;
        }
    }
}
