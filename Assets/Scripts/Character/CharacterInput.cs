using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpButton = "Jump";

    public float Move {  get; private set; }
    public bool Jump { get; private set; }

    private void Update()
    {
        Move = Input.GetAxisRaw(HorizontalAxis);
        Jump = Input.GetButtonDown(JumpButton);
    }
}
