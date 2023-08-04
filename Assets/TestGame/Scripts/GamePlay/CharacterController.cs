using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Vector3 _moveDirection;
    [SerializeField] private float _moveSpeed;
    private Animator _animator;
    private float _defaultSpeed;
    private Vector3 _newPosition;

    public Vector3 MoveDirection { get=>_moveDirection; set => _moveDirection = value; }

    public void Run()
    {
        _moveSpeed = _defaultSpeed;
    }

    public void Stop()
    {
        if (_moveSpeed == 0) return;
        _defaultSpeed = _moveSpeed;
        _moveSpeed = 0;
    }

    public void SetSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var newPosition = transform.position + _moveDirection * _moveSpeed;
        transform.localScale = new Vector3(_moveDirection.x, 1, 1);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        _animator.SetFloat("Speed", _moveSpeed);
    }
}
