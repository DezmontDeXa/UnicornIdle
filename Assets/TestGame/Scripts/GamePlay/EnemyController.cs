using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _takeDamageVFX;
    [SerializeField] private float _loosedMoveSpeed = 5;
    [SerializeField] private float _loosedDestroyAfter = 15f;
    private Animator _animator;
    private CharacterController _characterController;
    private EnemyAnimationEvents _enemyAnimEvents;
    private EnemyAttack _enemyAttack;
    private bool _hitted = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _characterController = GetComponentInChildren<CharacterController>();
        _enemyAnimEvents = GetComponentInChildren<EnemyAnimationEvents>();
        _enemyAttack = GetComponentInChildren<EnemyAttack>();

        _enemyAnimEvents.AngryFinished += OnAngryFinished;
        _enemyAttack.Attack += OnAttack;
    }

    private void OnAttack()
    {
        ShowWin();
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(x => x.GetComponent<EnemyController>().ShowWin());
        _enemyAttack.enabled = false;
    }

    private void ShowWin()
    {
        _animator.SetTrigger("Win");
        _characterController.Stop();
    }

    public void TakeDamage(Vector2 _hitPoint)
    {
        if (_hitted) return;
        _characterController.Stop();
        var vfx = Instantiate(_takeDamageVFX, new Vector3(_hitPoint.x, _hitPoint.y, 0), Quaternion.identity, transform);
        _hitted = true;
        _animator.SetTrigger("TakedDamage");
        _enemyAttack.enabled = false;
        StartCoroutine(DestroyAfter(_loosedDestroyAfter));
    }

    private IEnumerator DestroyAfter(float loosedDestroyAfter)
    {
        yield return new WaitForSeconds(loosedDestroyAfter);
        Destroy(gameObject);
    }

    private void OnAngryFinished()
    {
        _characterController.MoveDirection = new Vector3(1, 0, 0);
        _characterController.SetSpeed(_loosedMoveSpeed);
    }
}
