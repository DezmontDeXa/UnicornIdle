using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Vector3 _spawnDistanceOffset = new Vector3(10, 0, 0);
    [SerializeField] private int _sortingOrderOffset = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<PlayerController>(out var _))
        {
            Spawn(collision.transform.position + _spawnDistanceOffset);
            this.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + new Vector3(0, 1, 0), "enemy.png");
    }

    private void Spawn(Vector3 vector3)
    {
        var enemy = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Length)];
        var enemyObj = Instantiate(enemy, vector3, Quaternion.identity);
        enemyObj.transform.SetParent(transform, true);
        enemyObj.GetComponentInChildren<MeshRenderer>().sortingOrder += _sortingOrderOffset;
    }

}
