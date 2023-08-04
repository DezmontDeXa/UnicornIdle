using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _parallaxEffect;
    private float _prevTargetPositionX;

    private void Start()
    {
        _prevTargetPositionX = _target.position.x;
    }

    private void LateUpdate()
    {
        var delta = _target.position.x - _prevTargetPositionX;

        var newPosition = new  Vector3(transform.position.x + delta, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, _parallaxEffect);

        _prevTargetPositionX = _target.position.x;
    }
}
