using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private Animator _animator;

    private const string Speed = "Speed";
    private const string Attack = "Attack";
    private const string Jump = "Jump";
    private const string Grounded = "IsGrounded";
    private const string Block = "Block";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(Vector3 direction)
    {
        _animator.SetFloat(Speed, Mathf.Abs(direction.x) + Mathf.Abs(direction.z));
    }

    public void Attacking()
    {
        _animator.SetTrigger(Attack);
    }

    public void Jumping()
    {
        _animator.SetTrigger(Jump);
    }

    public void ChangeIsGrounded(bool isGrounded)
    {
        _animator.SetBool(Grounded, isGrounded);
    }

    public void Blocking()
    {
        _animator.SetTrigger(Block);
    }
}
