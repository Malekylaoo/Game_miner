using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private bool _isGrounded;
    //[SerializeField] private PlayerAnimations _playerAnimations;

    public bool Grounded => _isGrounded;

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
        //_playerAnimations.ChangeIsGrounded(_isGrounded);
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
        //_playerAnimations.ChangeIsGrounded(_isGrounded);
    }
}
