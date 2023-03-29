using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _damage;

    public void GiveDamage()
    {
        _player.TakeDamage(_damage);
    }
}
