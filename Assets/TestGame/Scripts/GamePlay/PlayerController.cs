using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;
    private PlayerAnimationEvents _playerAnimationEvents;
    private Gun _gun;
    private bool _isAttacking = false;
    private EnemyController _target;
    private Vector2 _hitPoint;
    private bool _isWin = false;

    public bool DamageTaked { get; private set; }

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponentInChildren<CharacterController>();
        _playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();
        _gun = GetComponentInChildren<Gun>();
        _playerAnimationEvents.FinishShoot += OnFinishShoot;
        _playerAnimationEvents.Muzzle += OnMuzzle;
    }

    private void Update()
    {
        if (_isAttacking) return;
        if (_isWin) return;
        if (DamageTaked) return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);

            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.transform.TryGetComponent<EnemyController>(out var enemy))
                    {
                        _isAttacking = true;
                        Attack(enemy, hit.point);
                        return;
                    }
                }
            }
        }
    }

    public void TakeDamage()
    {
        DamageTaked = true;
        _characterController.Stop();
        _animator.SetTrigger("Lose");
    }

    public void Win()
    {
        _characterController.Stop();
        _animator.SetTrigger("Win");
        _isWin = true;
    }

    private void Attack(EnemyController enemy, Vector2 point)
    {
        _characterController.Stop();
        _target = enemy;
        _hitPoint = point;
        _animator.SetTrigger("Shoot");
    }

    private void OnFinishShoot()
    {
        _characterController.Run();
    }

    private void OnMuzzle()
    {
        _gun.Shoot(_target, _hitPoint);
        _isAttacking = false;
    }
}
