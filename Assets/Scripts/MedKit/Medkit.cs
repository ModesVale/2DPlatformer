using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Medkit : MonoBehaviour
{
    [SerializeField] private int _healAmount = 20;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.Heal(_healAmount);
            gameObject.SetActive(false);
        }
    }
}
