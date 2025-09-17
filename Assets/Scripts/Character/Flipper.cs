using UnityEngine;

public class Flipper : MonoBehaviour
{
    private static readonly Quaternion LeftRotation = Quaternion.Euler(0f, 180f, 0f);
    private static readonly Quaternion RightRotation = Quaternion.Euler(0f, 0f, 0f);

    public void SetFacing(FacingDirection direction)
    {
        transform.rotation = direction == FacingDirection.Right ? RightRotation : LeftRotation;
    }
}