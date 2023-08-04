using UnityEngine;
using UnityEngine.Events;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent Finish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerController>(out var player))
        {
            player.Win();
            Finish?.Invoke();
            this.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + new Vector3(0, 1, 0), "finish.png");
    }

}
