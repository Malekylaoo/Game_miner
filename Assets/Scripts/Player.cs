using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimations))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _maxMana;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _maxCoolDownAttack;
    [SerializeField] private Transform _camera;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private GameObject _pointer;
    //[SerializeField] private Weapon _weapon;

    private float _currentStamina;
    private float _currentSpeed = 0;
    private float _currentMana;
    [SerializeField] private float _coolDownAttackTime;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private PlayerAnimations _playerAnimations;
    private Vector3 _movingVector;
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    private Vector3 _forward;
    private RaycastHit _hit;
    private WaitForSecondsRealtime _waitForSecondsRealtime = new WaitForSecondsRealtime(1.1f);

    public event UnityAction<float> HealthChanged;
    public event UnityAction<float> ManaChanged;
    public event UnityAction<float> StaminaChanged;

    public float CurrentHealth => _currentHealth;
    public float CurrentMana => _currentMana;
    public float CurrentStamina => _currentStamina;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimations = GetComponent<PlayerAnimations>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _currentMana = _maxMana;
        _currentHealth = _maxHealth;
        _currentStamina = _maxStamina;
        _coolDownAttackTime = 0;
    }

    private void Start()
    {
        _playerInput.Player.Jump.performed += ctx => OnJump();
        _playerInput.Player.Run.started += ctx => OnRun();
        _playerInput.Player.Run.canceled += ctx => OnWalk();
        _playerInput.Player.Walk.performed += ctx => OnWalk();
        _playerInput.Player.Interactive.performed += ctx => OnInteractive();
        _playerInput.Player.Block.performed += ctx => OnBlock();
    }

    private void FixedUpdate()
    {
        _moveDirection = _playerInput.Player.Walk.ReadValue<Vector3>();
        _forward = _camera.TransformDirection(Vector3.forward) * _attackDistance;
        Debug.DrawRay(_camera.position, _forward, Color.red);
        Debug.Log(_hit.collider);
        Look();
        Move(_currentSpeed);
        Debug.Log(_movingVector);
        //Debug.Log(_currentSpeed);
    }

    private void LateUpdate()
    {
        DrawPoint();
    }


    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Look()
    {
        _cameraForward = _camera.forward;
        _cameraRight = _camera.right;
        _cameraForward.y = 0;
        _cameraRight.y = 0;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _camera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles.x, _camera.rotation.eulerAngles.y, _rigidbody.rotation.eulerAngles.z);
    }

    private void Move(float speed)
    {
        if (_groundChecker.Grounded)
        {
            _movingVector = Vector3.ClampMagnitude(_cameraForward.normalized * _moveDirection.z * speed + _cameraRight.normalized * _moveDirection.x * speed, speed);
            _rigidbody.velocity = new Vector3(_movingVector.x, _rigidbody.velocity.y, _movingVector.z);
            _rigidbody.angularVelocity = Vector3.zero;
            _playerAnimations.Move(_movingVector);
        }
    }

    private void OnWalk()
    {
        _currentSpeed = _walkSpeed;
    }

    private void OnRun()
    {
        _currentSpeed = _runSpeed;
    }

    private void OnJump()
    {
        if (_groundChecker.Grounded)
        {
            //_playerAnimations.Jumping();
            _rigidbody.velocity = new Vector3(0, _jumpForce, 0);
        }   
    }

    private void OnAttack()
    {
        if (_groundChecker.Grounded)
        {
            //_playerAnimations.Attacking();

            if (Physics.Raycast(_camera.position, _forward, out _hit, _attackDistance) && _coolDownAttackTime <= 0)
            {
                _coolDownAttackTime = _maxCoolDownAttack;
                StartCoroutine(ChangeCoolDown());

                //if (_hit.transform.TryGetComponent(out Enemy enemy))
                    //_weapon.TurnOnCollider();
            }  
        }      
    }

    private void OnBlock()
    {
        //_playerAnimations.Blocking();
    }

    private IEnumerator ChangeCoolDown()
    {
        while(_coolDownAttackTime > 0)
        {
            yield return _waitForSecondsRealtime;
            _coolDownAttackTime = 0;
        }
    }

    private void OnInteractive()
    {
        if (Physics.Raycast(_camera.position, _forward, out _hit, _attackDistance))
        {
            if (_hit.transform.TryGetComponent(out IEntaractible interactive))
            {
                Debug.Log("ί ασπώ!");
                interactive.Interactive();
            }
        }
            
    }

    private void DrawPoint()
    {
        if (Physics.Raycast(_camera.position, _forward, out _hit, _attackDistance))
        {
            _pointer.SetActive(true);
            _pointer.transform.position = _hit.point;
        }
        else
        {
            _pointer.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public float GiveDamage()
    {
        return _damage;
    }

    /*public void Heal(float healValue)
    {
        _currentHealth += healValue;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void UseMagic(float manaValue)
    {
        _currentMana -= manaValue;
        _currentMana = Mathf.Clamp(_currentMana, 0, _maxMana);
        ManaChanged?.Invoke(_currentMana);
    }

    public void UseStamina(float staminaValue)
    {
        _currentStamina -= staminaValue;
        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
        StaminaChanged?.Invoke(_currentStamina);
    }*/
}
