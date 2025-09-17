using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Action _onPickedCallback;
    private bool _isPicked;

    public void RegisterReturn(Action returnAction)
    {
        _onPickedCallback = returnAction;
    }

    public void UnregisterReturn()
    {
        _onPickedCallback = null;
    }

    public void ResetState()
    {
        _isPicked = false;
        gameObject.SetActive(true);
    }

    public void PickUp()
    {
        if (_isPicked) return;

        _isPicked = true;
        gameObject.SetActive(false);
        _onPickedCallback?.Invoke();
    }
}
