using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundSensor : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;

    private int _contacts;

    public bool IsGrounded => _contacts > 0;
    public int ContactCount => _contacts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsInLayerMask(collision.gameObject.layer, _groundLayer))
        {
            _contacts++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInLayerMask(collision.gameObject.layer, _groundLayer))
        {
            _contacts = Mathf.Max(0, _contacts - 1);
        }
    }

    private static bool IsInLayerMask(int layer, LayerMask mask)
    {
        return (mask.value & (1 << layer)) != 0;
    }
}
