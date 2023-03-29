using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAnimations))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private Player _player;
    [SerializeField] private float _distance;
    [SerializeField] private float _distanceToAttack;

    private float _currentDistance;
    private EnemyAnimations _enemyAnimations;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _enemyAnimations = GetComponent<EnemyAnimations>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        Move();
        //Debug.Log(_currentDistance);
    }

    private void Move()
    {
        CheckDistance();
        Attack();
    }

    private void CheckDistance()
    {
        _currentDistance = Vector3.Distance(_player.transform.position, this.transform.position);

        if (_currentDistance >= _distance)
        {
            _navMeshAgent.enabled = false;
            _enemyAnimations.Moving(false);
        }
        else if(_currentDistance > _distanceToAttack)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_player.transform.position);
            _enemyAnimations.Moving(true);
        }
    }

    private void Attack()
    {
        if(_currentDistance <= _distanceToAttack)
        {
            _enemyAnimations.Moving(false);
            _enemyAnimations.Attacking();
            LookAt();
        }
    }

    private void LookAt()
    {
        transform.LookAt(_player.transform);
    }

    public void TakeDamage()
    {
        _health -= _player.GiveDamage();
        Debug.Log(_health);

        if (_health <= 0)
            Destroy(this.gameObject);
    }



}
