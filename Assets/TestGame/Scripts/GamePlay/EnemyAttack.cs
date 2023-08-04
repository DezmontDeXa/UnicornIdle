using UnityEngine;
using UnityEngine.Events;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _distance = 1f;
    [SerializeField] private Transform _attackInitPoint;

    public event UnityAction Attack;

    void Update()
    {
        var hits = Physics2D.RaycastAll(_attackInitPoint.position, Vector2.left, _distance);
        foreach (var hit in hits)
        {            
            if (hit.collider != null)
            {
                if (hit.transform.TryGetComponent<PlayerController>(out var player))
                {
                    if (player.DamageTaked) return;
                    player.TakeDamage();
                    Attack?.Invoke();
                }
            }
        }
    }
}
