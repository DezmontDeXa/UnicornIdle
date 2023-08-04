using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event UnityAction FinishShoot;
    public event UnityAction Muzzle;

    public void FinishShootAnimEvent()
    {
        FinishShoot?.Invoke();
    }

    public void MuzzleAnimEvent()
    {
        Muzzle?.Invoke();
    }
}
