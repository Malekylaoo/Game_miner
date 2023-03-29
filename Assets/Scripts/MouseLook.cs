using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensetive;
    [SerializeField] private Transform _player;
    [SerializeField] private float _minRotation;
    [SerializeField] private float _maxRotation;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation;


    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _mouseX = Input.GetAxis("Mouse X") * _mouseSensetive * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _mouseSensetive * Time.deltaTime;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minRotation, _maxRotation);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _player.Rotate(Vector3.up * _mouseX);
    }
}
