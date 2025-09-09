using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    private UnityAction _returnAction;
    private bool _picked;

    public void RegisterReturn(UnityAction returnAction)
    {
        _returnAction = returnAction;
    }

    public void ResetState()
    {
        _picked = false;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterMove player))
        {
            if (_picked) return;

            _picked = true;
            gameObject.SetActive(false);
            _returnAction?.Invoke();
        }
    }
}
