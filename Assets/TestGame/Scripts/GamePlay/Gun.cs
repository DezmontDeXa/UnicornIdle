using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _shootDelay = 0.3f;
    [SerializeField] private ParticleSystem _muzzleFx;
    [SerializeField] private Light2D _muzzleLight;
    [SerializeField] private Transform _muzzlePoint;

    public void Shoot(EnemyController _target, Vector2 hitPoint)
    {
        var particle = Instantiate(_muzzleFx, _muzzlePoint);
        _muzzleLight.enabled = true;
        StartCoroutine(DestroyMuzzle(particle));
        StartCoroutine(DelayedHit(_target, hitPoint));
    }
    private IEnumerator DelayedHit(EnemyController target, Vector2 hitPoint)
    {
        yield return new WaitForSeconds(_shootDelay);
        target.TakeDamage(hitPoint);
        _muzzleLight.enabled = false;
    }

    private IEnumerator DestroyMuzzle(ParticleSystem particle)
    {
        yield return new WaitForSeconds(3);
        Destroy(particle.gameObject);
    }
}
