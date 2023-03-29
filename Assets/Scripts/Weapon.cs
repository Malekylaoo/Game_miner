using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Collider _collider;

    public Collider ColliderIsOn => _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage();
            _collider.enabled = false;
            Debug.Log("Я попал по врагу!!!!");
        }
        Debug.Log("Я не попал о врагу");
    }

    public void TurnOnCollider()
    {
        _collider.enabled = true;
    }    
}
