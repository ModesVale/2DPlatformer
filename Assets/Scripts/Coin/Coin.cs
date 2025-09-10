using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Action _returnAction;
    private bool _picked;

    public void RegisterReturn(Action returnAction)
    {
        _returnAction = returnAction;
    }

    public void ResetState()
    {
        _picked = false;
        gameObject.SetActive(true);
    }

    public void PickUp()
    {
        if (_picked) return;

        _picked = true;
        gameObject.SetActive(false);
        _returnAction?.Invoke();
    }
}
