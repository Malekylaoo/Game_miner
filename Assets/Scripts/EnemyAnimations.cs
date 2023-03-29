using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;

    private const string Move = "IsMoving";
    private const string Attack = "Attack";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Moving(bool isMoving)
    {
        _animator.SetBool(Move, isMoving);
    }    

    public void Attacking()
    {
        _animator.SetTrigger(Attack);
    }    

    
}
