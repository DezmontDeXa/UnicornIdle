using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEvents : MonoBehaviour
{
    public event UnityAction AngryFinished;
    public void RaiseAngryFinished()
    {
        AngryFinished.Invoke();
    }
}
